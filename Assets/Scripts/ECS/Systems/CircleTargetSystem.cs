using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct CircleTargetSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (localTransform, mob, circleTarget, unitMover, entity) in SystemAPI
                     .Query<RefRW<LocalTransform>, RefRW<Mob>, RefRW<CircleTarget>, RefRW<UnitMover>>().WithEntityAccess()
                     .WithOptions(EntityQueryOptions.IgnoreComponentEnabledState))
        {
            if (!SystemAPI.TryGetSingletonBuffer(out DynamicBuffer<GameObjectInfo> goInfoBuffer))
                continue;

            float3 targetPos = default; 
            
            foreach (var goInfo in goInfoBuffer)
            {
                if (goInfo.ObjectType == mob.ValueRO.MobTarget)
                {
                    targetPos = goInfo.Position;
                    break;
                }
            }

            float distanceToTargetSq = math.distancesq(localTransform.ValueRO.Position, targetPos);
            
            // If close enough to start circling
            if (distanceToTargetSq <= circleTarget.ValueRO.StartCircleDistanceSq)
            {
                // Enable UnitMover to handle movement
                state.EntityManager.SetComponentEnabled<UnitMover>(entity, true);
                
                // Calculate the direction from target to mob
                float3 offset = localTransform.ValueRO.Position - targetPos;
                float currentDistance = math.sqrt(distanceToTargetSq);
                
                // Shrink the radius over time
                circleTarget.ValueRW.Radius = math.max(0f, circleTarget.ValueRO.Radius - circleTarget.ValueRO.ShrinkSpeed * SystemAPI.Time.DeltaTime);
                
                // Calculate rotation around the target
                quaternion rotation = quaternion.AxisAngle(
                    math.up(),
                    circleTarget.ValueRO.OrbitSpeed * SystemAPI.Time.DeltaTime
                );

                // Rotate the offset around the target
                float3 rotatedOffset = math.mul(rotation, offset);
                
                // Normalize and apply the desired radius
                float3 newOffset = math.normalize(rotatedOffset) * circleTarget.ValueRO.Radius;
                float3 newPos = targetPos + newOffset;

                // Set the new target position for UnitMover
                unitMover.ValueRW.targetPosition = newPos;
                unitMover.ValueRW.TargetOverrideActive = true;
            }
            else
            {
                // Too far from target, move normally toward it
                state.EntityManager.SetComponentEnabled<UnitMover>(entity, true);
                unitMover.ValueRW.targetPosition = targetPos;
                unitMover.ValueRW.TargetOverrideActive = false;
            }
        }
    }
}