using Unity.Entities;
using UnityEngine;

public struct EntitiesReferences : IComponentData
{
    public Entity MobPrefabEntity;
}

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    private GameObject MobPrefabGameObject;
    
    public class Baker : Baker<EntitiesReferencesAuthoring>
    {
        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitiesReferences
            {
                MobPrefabEntity = GetEntity(authoring.MobPrefabGameObject, TransformUsageFlags.Dynamic),
            });
        }
    }
}
