using Unity.Entities;
using UnityEngine;

public struct GameRunningTag : IComponentData { }

public class GameRunningTagAuthoring : MonoBehaviour
{
    public class Baker : Baker<GameRunningTagAuthoring>
    {
        public override void Bake(GameRunningTagAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new GameRunningTag());
        }
    }
}
