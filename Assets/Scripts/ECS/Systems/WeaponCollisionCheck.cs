using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]
internal partial struct WeaponCollisionCheck : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        SimulationSingleton simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
        
        // Complete the physics simulation dependency to safely read trigger events
        state.Dependency.Complete();
        
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        
        WeaponManager weaponManager = SystemAPI.GetSingleton<WeaponManager>();

        int i = 0;
        foreach(ISimulationEvent<TriggerEvent> triggerEvent in simulationSingleton.AsSimulation().TriggerEvents)
        {
            Debug.Log("Number of triggers: " + i);
            i++;

            // Entity entityA = triggerEvent.EntityA;
            // Entity entityB = triggerEvent.EntityB;
            //
            // // Example: Check if one is a weapon and the other is a mob
            // bool aIsWeapon = state.EntityManager.HasComponent<Weapon>(entityA);
            // bool bIsMob = state.EntityManager.HasComponent<Mob>(entityB);
            //
            // bool bIsWeapon = state.EntityManager.HasComponent<Weapon>(entityB);
            // bool aIsMob = state.EntityManager.HasComponent<Mob>(entityA);
            //
            // Debug.Log("Hit triggered!");
            //
            // if (aIsWeapon && bIsMob)
            // {
            //     Entity mobEntity = entityB;
            //     // Handle hit mob
            //     Entity damageEvent = ecb.CreateEntity();
            //     ecb.AddComponent(damageEvent, new MobDamageTakenEvent
            //     {
            //         Amount = weaponManager.DamagePerHit,
            //         Entity = mobEntity,
            //     });
            // }
            // else if (bIsWeapon && aIsMob)
            // {
            //     Entity mobEntity = entityA;
            //
            //     Entity damageEvent = ecb.CreateEntity();
            //     ecb.AddComponent(damageEvent, new MobDamageTakenEvent
            //     {
            //         Amount = weaponManager.DamagePerHit,
            //         Entity = mobEntity,
            //     });
            // }
        }
    }
}
