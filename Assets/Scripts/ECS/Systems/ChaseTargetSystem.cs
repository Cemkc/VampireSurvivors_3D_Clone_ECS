using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

partial struct ChaseTargetSystem : ISystem
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
        EntityCommandBuffer ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (unitMover, chaseTarget, entity)
                 in SystemAPI.Query<RefRW<UnitMover>, RefRO<ChaseTargetComponent>>().WithEntityAccess())
        {
            if (!SystemAPI.TryGetSingletonBuffer(out DynamicBuffer<GameObjectInfo> goInfoBuffer))
                return;

            GameObjectInfo target = default;
            bool targetMatch = false;

            foreach (var goInfo in goInfoBuffer)
            {
                if (chaseTarget.ValueRO.TargetObjectType == goInfo.ObjectType)
                {
                    target = goInfo;
                    targetMatch = true;
                    break;
                }
            }

            if (!targetMatch) continue;
            
            if (!unitMover.ValueRO.TargetOverrideActive)
            {
                unitMover.ValueRW.targetPosition = target.Position;   
            }
            
        }

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

            for (int i = 0; i < GoInfoCount; i++)
            {
                if (chaseTargetComponent.TargetObjectType == GoInfos[i].ObjectType)
                {
                    target = GoInfos[i];
                    targetFound = true;
                    break;
                }
            }

            if (!targetFound)
                return;

            if (!unitMover.TargetOverrideActive)
            {
                unitMover.targetPosition = target.Position;   
            }
        } 
    }
}
