using Unity.Entities;
using UnityEngine;

public struct GamePausedTag : IComponentData { }

public class GamePausedTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<GamePausedTagAuthoring>
    {
        public override void Bake(GamePausedTagAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new GamePausedTag());
        }
    }
}