using Unity.Entities;
using UnityEngine;

public struct CircleTarget : IComponentData
{
    public float StartCircleDistanceSq;
    public float OrbitSpeed;
    public float ShrinkSpeed;
    public float Radius;
}

public class CircleTargetAuthoring : MonoBehaviour
{
    public float StartCircleDistanceSq;
    public float OrbitSpeed;
    public float ShrinkSpeed;
    
    public class Baker : Baker<CircleTargetAuthoring>
    {
        public override void Bake(CircleTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CircleTarget
            {
                StartCircleDistanceSq = authoring.StartCircleDistanceSq,
                Radius = Mathf.Sqrt(authoring.StartCircleDistanceSq),
                OrbitSpeed = authoring.OrbitSpeed,
            });
            
        }
    }
    
}
    