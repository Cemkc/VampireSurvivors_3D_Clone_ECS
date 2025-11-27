using Unity.Entities;
using UnityEngine;

public struct OrbitAgent : IComponentData
{
    public GameObjectType TargetObjectType;
    public float OrbitStartDistanceSq;
    public float MoveSpeed;
    public float OrbitSpeed;
    public float InwardSpeed;
}

public class OrbitAgentAuthoring : MonoBehaviour
{
    public GameObjectType TargetObjectType;
    public float OrbitStartDistanceSq;
    public float MoveSpeed;
    public float OrbitSpeed;
    public float InwardSpeed;
    
    public class Baker : Baker<OrbitAgentAuthoring>
    {
        public override void Bake(OrbitAgentAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new OrbitAgent 
            {
                TargetObjectType = authoring.TargetObjectType,
                MoveSpeed = authoring.MoveSpeed,
                OrbitSpeed = authoring.OrbitSpeed,
                InwardSpeed = authoring.InwardSpeed,
                OrbitStartDistanceSq = authoring.OrbitStartDistanceSq,
            });
        }
    }
}
    