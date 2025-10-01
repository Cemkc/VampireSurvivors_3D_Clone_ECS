using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct HitCharacterSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        
        foreach (var (localTransform, unitMover, hitTarget, entity) in SystemAPI
                     .Query<RefRO<LocalTransform>, RefRO<UnitMover>, RefRO<HitTarget>>().WithEntityAccess())
        {
            float3 direction = unitMover.ValueRO.targetPosition - localTransform.ValueRO.Position;
            float distanceSq = math.lengthsq(direction);

            // hitTarget.ValueRO.HitDistance made sure to be greater than UnitMoverSystem stop distance.
            if (distanceSq <= hitTarget.ValueRO.HitDistance)
            {
                Debug.Log("Close Enough");
                Entity e = ecb.CreateEntity();
                ecb.AddComponent(e, new DamageEvent
                {
                    id = hitTarget.ValueRO.TargetID,
                    amount = hitTarget.ValueRO.DamageAmount,
                });
                
                ecb.DestroyEntity(entity);
            }
        }
    }
}
