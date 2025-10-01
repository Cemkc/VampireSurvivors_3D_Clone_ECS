using Unity.Entities;
using UnityEngine;

public struct CharacterCollider : IComponentData
{
    
}

public class CharacterColliderAuthoring : MonoBehaviour
{
    public class Baker : Baker<CharacterColliderAuthoring>
    {
        public override void Bake(CharacterColliderAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CharacterCollider());
        }
    }
}
