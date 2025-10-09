using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[MaterialProperty("_DigitIndex")]
public struct PixelDigitMatOverride : IComponentData
{
    public float DigitIndex;
}

public class PixelDigitOverrideAuthoring : MonoBehaviour
{
    [Range(0, 9)]
    public int Value;
    
    public class Baker : Baker<PixelDigitOverrideAuthoring>
    {
        public override void Bake(PixelDigitOverrideAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PixelDigitMatOverride{DigitIndex = authoring.Value});
        }
    }
}
