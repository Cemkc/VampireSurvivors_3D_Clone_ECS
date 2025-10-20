
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

public struct FaceTheCamera : IComponentData
{
    
}

public class FaceTheCameraAuthoring : MonoBehaviour
{
    private class FaceTheCameraBaker : Baker<FaceTheCameraAuthoring>
    {
        public override void Bake(FaceTheCameraAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new FaceTheCamera());
        }
    }
}
    