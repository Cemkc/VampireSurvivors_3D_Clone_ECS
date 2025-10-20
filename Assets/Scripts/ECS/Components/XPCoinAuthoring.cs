using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct XPCoin : IComponentData
{
    public GameObjectType TargetType;
    public int XPAmount;
    public float MagnetPullDistanceSq;
    public float3 TargetPosition;
}

public class XPCoinAuthoring : MonoBehaviour
{
    public GameObjectType TargetType;
    public float MagnetPullDistanceSq;
    public int XPAmount;
    
    public class Baker : Baker<XPCoinAuthoring>
    {
        public override void Bake(XPCoinAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new XPCoin
            {
                TargetType = authoring.TargetType,
                MagnetPullDistanceSq = authoring.MagnetPullDistanceSq,
                XPAmount = authoring.XPAmount,
            });
        }
    }
}