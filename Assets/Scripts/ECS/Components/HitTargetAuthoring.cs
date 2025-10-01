using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public struct HitTarget : IComponentData
{
    public int TargetID;
    public int DamageAmount;
    public float HitDistance;
}

public class HitTargetAuthoring : MonoBehaviour
{
    public int TargetObjectID;
    public int DamageAmount;

    [Range(UnitMoverSystem.REACHED_TARGET_POSITION_DISTANCE_SQ, 50f)]
    public float HitDistance;
    
    public class Baker : Baker<HitTargetAuthoring>
    {
        public override void Bake(HitTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HitTarget
            {
                TargetID = authoring.TargetObjectID,
                DamageAmount = authoring.DamageAmount,
                HitDistance = authoring.HitDistance,
            });
        }
    }
}
