using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct EntityReferences : IComponentData
{
    public Entity MobPrefabEntity;
    public Entity DamageDigitPrefabEntity;
    public Entity XPCollectable;
    
    public Random Random;
}

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    public GameObject MobPrefabGameObject;
    public GameObject DamageDigitPrefabGameObject;
    public GameObject XPCollectable;
    
    public class Baker : Baker<EntitiesReferencesAuthoring>
    {
        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntityReferences
            {
                MobPrefabEntity = GetEntity(authoring.MobPrefabGameObject, TransformUsageFlags.Dynamic),
                DamageDigitPrefabEntity = GetEntity(authoring.DamageDigitPrefabGameObject, TransformUsageFlags.Dynamic),
                XPCollectable = GetEntity(authoring.XPCollectable, TransformUsageFlags.Dynamic),
                Random = new Random((uint)UnityEngine.Random.Range(0, 1000)),
            });
        }
    }
}
