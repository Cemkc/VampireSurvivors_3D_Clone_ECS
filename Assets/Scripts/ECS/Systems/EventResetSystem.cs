using Unity.Burst;
using Unity.Entities;
using UnityEngine.EventSystems;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
internal partial struct EventResetSystem : ISystem
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
        
        foreach (var (damageTakenEvent, entity) in SystemAPI.Query<RefRW<MobDamageTakenEvent>>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }
    }
}
    