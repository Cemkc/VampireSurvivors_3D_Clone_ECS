using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public struct Mob : IComponentData { }

public struct ChaseTargetComponent : IComponentData
{
    public GameObjectType GameObjectType;
    public int DamageAmount;
    public float HitDistance;
}

public class MobAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("TargetType")] public GameObjectType gameObjectType;
    public int DamageAmount;
    [Range(UnitMoverSystem.REACHED_TARGET_POSITION_DISTANCE_SQ, 50f)]
    public float HitDistance;
    
    public class Baker : Baker<MobAuthoring>
    {
        public override void Bake(MobAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ChaseTargetComponent
            {
                GameObjectType = authoring.gameObjectType,
                DamageAmount = authoring.DamageAmount,
                HitDistance = authoring.HitDistance,
            });
            
            AddComponent(entity, new Mob());
        }
    }
}
