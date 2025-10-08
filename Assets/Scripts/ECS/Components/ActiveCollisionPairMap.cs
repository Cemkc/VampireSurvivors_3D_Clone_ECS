using Unity.Collections;
using Unity.Entities;

public struct ActiveCollisionPairMap : IComponentData
{
    public NativeParallelHashMap<int, NativeParallelHashSet<int>> Map;
}
