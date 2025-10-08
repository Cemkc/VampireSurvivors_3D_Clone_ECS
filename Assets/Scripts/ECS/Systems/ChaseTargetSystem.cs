using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct ChaseTargetSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        if (!SystemAPI.TryGetSingletonBuffer(out DynamicBuffer<GameObjectInfo> goInfoBuffer))
        {
            return;
        }
        
        foreach (var (localTransform, unitMover, chaseTargetComponent, entity) in SystemAPI
                     .Query<RefRO<LocalTransform>, RefRW<UnitMover>, RefRO<ChaseTargetComponent>>().WithEntityAccess())
        {
            // GameObjectInfo target = default;
            // bool targetMatch = false;
            //
            // foreach (var goInfo in goInfoBuffer)
            // {
            //     if (chaseTargetComponent.ValueRO.GameObjectType == goInfo.ObjectType)
            //     {
            //         target = goInfo;
            //         targetMatch = true;
            //         break;
            //     }
            // }
            //
            // if(!targetMatch) continue;
            //
            // UnitMover unitMoverComponent = unitMover.ValueRO;
            // unitMoverComponent.targetPosition = target.Position;
            // unitMover.ValueRW = unitMoverComponent;
            //
            // float3 direction = target.Position - localTransform.ValueRO.Position;
            // float distanceSq = math.lengthsq(direction);
            //
            // // chaseTargetComponent.ValueRO.HitDistance made sure to be greater than UnitMoverSystem stop distance.
            // if (distanceSq <= chaseTargetComponent.ValueRO.HitDistance)
            // {
            //     Entity e = ecb.CreateEntity();
            //     ecb.AddComponent(e, new MobDamageGivenEvent
            //     {
            //         Id = target.ID,
            //         Amount = chaseTargetComponent.ValueRO.DamageAmount,
            //     });
            //     
            //     ecb.DestroyEntity(entity);
            // }
        }
    }
}
