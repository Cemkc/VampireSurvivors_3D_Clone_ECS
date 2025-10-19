using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[MaterialProperty("_DigitIndex"), MaterialProperty("_Pulse")]
public struct DigitValueMatOverride : IComponentData
{
    public float DigitIndex;
}

[MaterialProperty("_Pulse")]
public struct DigitPulseMatOverride : IComponentData
{
    public float Pulse;
}

public class PixelDigitOverrideAuthoring : MonoBehaviour
{
    [Range(0, 9)]
    public int Value;
    [Range(0, 1)]
    public int Pulse;
    
    public class Baker : Baker<PixelDigitOverrideAuthoring>
    {
        public override void Bake(PixelDigitOverrideAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DigitValueMatOverride
            {
                DigitIndex = authoring.Value,
            });
            
            AddComponent(entity, new DigitPulseMatOverride
            {
                Pulse = authoring.Pulse,
            });
        }
    }
}
