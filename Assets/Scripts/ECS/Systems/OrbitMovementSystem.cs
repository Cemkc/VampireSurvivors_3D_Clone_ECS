using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct OrbitMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        state.RequireForUpdate<GameRunningTag>();
    }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach (var (transform, agent) 
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<OrbitAgent>>())
        {
            if (!SystemAPI.TryGetSingletonBuffer(out DynamicBuffer<GameObjectInfo> goInfoBuffer))
                return;

            float3 targetPosition = default;
            var goInfos = goInfoBuffer.AsNativeArray();

            foreach (var goInfo in goInfos)
            {
                if (goInfo.ObjectType == agent.ValueRO.TargetObjectType)
                {
                    targetPosition = goInfo.Position;
                }
            }

            float3 dir = targetPosition - transform.ValueRO.Position;
            float dist = math.lengthsq(dir);

            float3 newPos = transform.ValueRO.Position;

            // 1. MOVE TOWARDS TARGET
            if (dist > agent.ValueRO.OrbitStartDistanceSq)
            {
                float3 moveDir = math.normalize(dir);
                newPos += moveDir * agent.ValueRO.MoveSpeed * dt;
            }
            // 2. ORBIT & SPIRAL INWARD
            else
            {
                float3 toTarget = math.normalize(dir);

                // Get perpendicular tangent for orbit
                float3 tangent = math.normalize(math.cross(toTarget, new float3(0,1,0)));

                float3 orbitMovement = tangent * agent.ValueRO.OrbitSpeed * dt;
                float3 inwardMovement = toTarget * agent.ValueRO.InwardSpeed * dt;

                newPos += orbitMovement + inwardMovement;
            }

            transform.ValueRW.Position = newPos;

            transform.ValueRW.Rotation = quaternion.LookRotationSafe(math.normalize(dir), math.up());
        }
    }
}