using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct UnitMover : IComponentData, IEnableableComponent
{
    public bool TargetOverrideActive;
    public float moveSpeed;
    public float rotationSpeed;
    public float3 targetPosition;
    public bool LookAtTarget;
}

public class UnitMoverAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public bool LookAtTarget = true;
    public bool Enabled = true;
    
    public class Baker : Baker<UnitMoverAuthoring>
    {
        public override void Bake(UnitMoverAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitMover {
                TargetOverrideActive = false,
                moveSpeed = authoring.moveSpeed,
                rotationSpeed = authoring.rotationSpeed,
                LookAtTarget = authoring.LookAtTarget,
            });

            if (!authoring.Enabled)
            {
                SetComponentEnabled<UnitMover>(entity, false);
            }
        }
    }
}
