using Unity.Entities;
using UnityEngine;

public struct ChaseTargetComponent : IComponentData
{
    public GameObjectType TargetObjectType;
}

public class ChaseTargetAuthoring : MonoBehaviour
{
    public GameObjectType TargetObjectType;
    
    private class Baker : Baker<ChaseTargetAuthoring>
    {
        public override void Bake(ChaseTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ChaseTargetComponent
            {
                TargetObjectType = authoring.TargetObjectType,
            });
            
        }
    }
}