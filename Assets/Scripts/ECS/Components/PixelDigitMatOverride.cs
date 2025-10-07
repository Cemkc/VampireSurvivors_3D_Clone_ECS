using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

[MaterialProperty("_DigitIndex")]
public struct PixelDigitMatOverride : IComponentData
{
    public float DigitIndex;
}

