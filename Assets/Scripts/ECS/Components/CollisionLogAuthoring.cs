using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(0)]
public struct CollisionLog : IBufferElementData
{
    public int CollidedEntityId;
}

public class CollisionLogAuthoring : MonoBehaviour
{
    public class Baker : Baker<CollisionLogAuthoring>
    {
        public override void Bake(CollisionLogAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            DynamicBuffer<CollisionLog> buffer = AddBuffer<CollisionLog>(entity);
        }
    }
}
