using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

public struct ChaseTargetComponent : IComponentData
{
    public TargetType TargetType;
    public int DamageAmount;
    public float HitDistance;
}

public class ChaseTargetAuthoring : MonoBehaviour
{
    public TargetType TargetType;
    public int DamageAmount;
    [Range(UnitMoverSystem.REACHED_TARGET_POSITION_DISTANCE_SQ, 50f)]
    public float HitDistance;
    
    public class Baker : Baker<ChaseTargetAuthoring>
    {
        public override void Bake(ChaseTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ChaseTargetComponent
            {
                TargetType = authoring.TargetType,
                DamageAmount = authoring.DamageAmount,
                HitDistance = authoring.HitDistance,
            });
        }
    }
}
