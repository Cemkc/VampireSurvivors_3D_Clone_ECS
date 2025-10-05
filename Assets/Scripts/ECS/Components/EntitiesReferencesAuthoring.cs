using Unity.Entities;
using UnityEngine;

public struct EntityReferences : IComponentData
{
    public Entity MobPrefabEntity;
}

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    public GameObject MobPrefabGameObject;
    
    public class Baker : Baker<EntitiesReferencesAuthoring>
    {
        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntityReferences
            {
                MobPrefabEntity = GetEntity(authoring.MobPrefabGameObject, TransformUsageFlags.Dynamic),
            });
        }
    }
}
