using NUnit.Framework;
using Unity.Burst;
using Unity.Collections;
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
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

        // Get the singleton buffer as a read-only native array.
        if (!SystemAPI.TryGetSingletonBuffer(out DynamicBuffer<GameObjectInfo> goInfoBuffer))
            return;

        var goInfos = goInfoBuffer.AsNativeArray(); // zero-cost view of buffer
        var goInfoCount = goInfos.Length;

        var job = new ChaseTargetJob
        {
            ECB = ecb,
            GoInfos = goInfos,
            GoInfoCount = goInfoCount
        };

        job.ScheduleParallel();
        
    }
    
    [BurstCompile]
    public partial struct ChaseTargetJob : IJobEntity
    {
        [ReadOnly] public NativeArray<GameObjectInfo> GoInfos;
        [ReadOnly] public int GoInfoCount;
        public EntityCommandBuffer.ParallelWriter ECB;

        void Execute(
            [ChunkIndexInQuery] int chunkIndex,
            Entity entity,
            in LocalTransform localTransform,
            ref UnitMover unitMover,
            in ChaseTargetComponent chaseTargetComponent)
        {
            GameObjectInfo target = default;
            bool targetFound = false;

            // Find the target once — linear search, can optimize later if needed.
            for (int i = 0; i < GoInfoCount; i++)
            {
                if (chaseTargetComponent.GameObjectType == GoInfos[i].ObjectType)
                {
                    target = GoInfos[i];
                    targetFound = true;
                    break;
                }
            }

            if (!targetFound)
                return;

            unitMover.targetPosition = target.Position;

            float3 direction = target.Position - localTransform.Position;
            float distanceSq = math.lengthsq(direction);

            if (distanceSq <= chaseTargetComponent.HitDistance)
            {
                Entity e = ECB.CreateEntity(chunkIndex);
                ECB.AddComponent(chunkIndex, e, new MobDamageGivenEvent
                {
                    Id = target.ID,
                    Amount = chaseTargetComponent.DamageAmount
                });

                ECB.DestroyEntity(chunkIndex, entity);
            }
        }
    }
}
