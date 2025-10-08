using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

internal partial struct WeaponSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        WeaponManager weaponManager = SystemAPI.GetSingleton<WeaponManager>();
        
        DynamicBuffer<GameObjectInfo> objectInfoBuffer = SystemAPI.GetSingletonBuffer<GameObjectInfo>();
        GameObjectInfo gameObjectInfo = default;
        
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);
        
        PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        CollisionWorld collisionWorld = physicsWorldSingleton.CollisionWorld;
        NativeList<DistanceHit> distanceHitList = new NativeList<DistanceHit>(Allocator.Temp);
        
        foreach (var objectInfo in objectInfoBuffer)
        {
            if (objectInfo.ObjectType == GameObjectType.Character1)
            {
                gameObjectInfo = objectInfo;
            }
        }
        
        foreach (var (localTransform, weapon) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRW<Weapon>>())
        {
            float3 pivot = gameObjectInfo.Position;
    
            //weapon.ValueRW.Angle += weapon.ValueRO.RotateSpeed * SystemAPI.Time.DeltaTime;
            float angle = weapon.ValueRO.Angle + ((math.PI2 / weaponManager.NumberOfWeapons) * weapon.ValueRO.Number);

            float3 positionOnCircle = new float3(math.sin(angle), 0.0f, math.cos(angle));
            float3 weaponPosition = pivot + positionOnCircle * weaponManager.Radius;

            float3 weaponDirection = math.normalize(weaponPosition - pivot); 

            localTransform.ValueRW.Position = weaponPosition;
            localTransform.ValueRW.Rotation = quaternion.LookRotation(weaponDirection, new float3(0.0f, 1.0f, 0.0f));
            localTransform.ValueRW.Scale = 1.0f;
            
            // distanceHitList.Clear();
            // CollisionFilter collisionFilter = new CollisionFilter
            // {
            //     BelongsTo = ~0u,
            //     CollidesWith = 1u << 6,
            //     GroupIndex = 0,
            // };
            //
            // if (collisionWorld.OverlapBox(localTransform.ValueRO.Position, quaternion.identity,
            //         new float3(0.5f, 0.5f, 0.5f), ref distanceHitList, collisionFilter))
            // {
            //     Debug.Log("Yes something collided.");
            // }

        }
    }
}
    