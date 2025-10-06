using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameObjectType
{
    None,
    Character1,
}

[InternalBufferCapacity(1)]
public struct GameObjectInfo : IBufferElementData
{
    public GameObjectType ObjectType;
    public float3 Position;
    public int ID;
}

public class GameObjectInfoAuthoring : MonoBehaviour
{
    [FormerlySerializedAs("MobTargetTypes")] public List<GameObjectType> GameObjectTypes;
    
    public class Baker : Baker<GameObjectInfoAuthoring>
    {
        public override void Bake(GameObjectInfoAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            DynamicBuffer<GameObjectInfo> buffer = AddBuffer<GameObjectInfo>(entity);

            foreach (var gameObjectType in authoring.GameObjectTypes)
            {
                buffer.Add(new GameObjectInfo
                {
                    ObjectType = gameObjectType,
                    Position = float3.zero,
                    ID = 0,
                });
            }
        }
    }
}

