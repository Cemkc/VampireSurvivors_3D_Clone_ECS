using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Weapon : IComponentData
{
    public float3 Pivot;
    public float Radius;
    public float Number;
    public float RotateSpeed;
    public float Angle;
    public bool ClockWise;
}

public class WeaponAuthoring : MonoBehaviour
{
    public class Baker : Baker<WeaponAuthoring>
    {
        public override void Bake(WeaponAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Weapon
            {
            });
        }
    }
}

    