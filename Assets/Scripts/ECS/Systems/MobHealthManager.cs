using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

internal partial struct MobHealthManager : ISystem
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
        
        var mobs = SystemAPI.GetComponentLookup<Mob>(false);
        var materialOverride = SystemAPI.GetComponentLookup<PixelDigitMatOverride>(false);
        var mobsChildBuffer = SystemAPI.GetBufferLookup<Child>(false);
        
        foreach (var damageTakenEvent in SystemAPI.Query<RefRW<MobDamageTakenEvent>>())
        {
            Entity mobEntity = damageTakenEvent.ValueRO.Entity;
            Mob mobData = mobs[mobEntity];
            
            mobData.Health -= damageTakenEvent.ValueRO.Amount;

            DynamicBuffer<Child> childrenBuffer = mobsChildBuffer[mobEntity];

            foreach (var child in childrenBuffer)
            {
                if (SystemAPI.HasComponent<PixelDigitMatOverride>(child.Value))
                {
                    materialOverride[child.Value] = new PixelDigitMatOverride{DigitIndex = 7};
                }
            }
            
            // Debug.Log($"Frame: {Time.frameCount}: {mobEntity.Index} Mob has taken {damageTakenEvent.ValueRO.Amount} damage. Health is: {mobData.Health}");
            
            if (mobData.Health <= 0)
            {
                // Debug.Log("Mob is being destroyed.");
                ecb.DestroyEntity(mobEntity);
            }

            mobs[mobEntity] = mobData; 
        }
    }
}
    