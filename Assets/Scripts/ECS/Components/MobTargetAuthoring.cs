using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public enum TargetType
{
    None,
    Character1,
}

[InternalBufferCapacity(1)]
public struct MobTarget : IBufferElementData
{
    public TargetType TargetType;
    public float3 Position;
    public int ID;
}

public class MobTargetAuthoring : MonoBehaviour
{
    public List<TargetType> MobTargetTypes;
    
    public class Baker : Baker<MobTargetAuthoring>
    {
        public override void Bake(MobTargetAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            DynamicBuffer<MobTarget> buffer = AddBuffer<MobTarget>(entity);

            foreach (var mobTargetType in authoring.MobTargetTypes)
            {
                buffer.Add(new MobTarget
                {
                    TargetType = mobTargetType,
                    Position = float3.zero,
                    ID = 0,
                });
            }
        }
    }
}

