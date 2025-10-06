using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public struct ChaseTargetComponent : IComponentData
{
    public GameObjectType GameObjectType;
    public int DamageAmount;
    public float HitDistance;
}

public class ChaseTargetAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("TargetType")] public GameObjectType gameObjectType;
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
                GameObjectType = authoring.gameObjectType,
                DamageAmount = authoring.DamageAmount,
                HitDistance = authoring.HitDistance,
            });
        }
    }
}
