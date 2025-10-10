
using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

internal partial struct DamageDigitSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float3 cameraForward = float3.zero;
        if (Camera.main != null)
        {
            cameraForward = Camera.main.transform.forward;
        }
        
        var parentLookup = SystemAPI.GetComponentLookup<Parent>(true);
        var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true);
        
        foreach (var (localTransform, damageDigit, entity) in SystemAPI
                     .Query<RefRW<LocalTransform>, RefRO<DamageDigit>>().WithEntityAccess())
        {
            Entity parentEntity = GetRootParent(entity, parentLookup);

            LocalTransform parentTransform;
            if (parentEntity == entity)
            {
                parentTransform = localTransform.ValueRO;
            }
            else
            {
                parentTransform = transformLookup[parentEntity];
            }

            localTransform.ValueRW.Rotation =
                parentTransform.InverseTransformRotation(quaternion.LookRotation(cameraForward, math.up()));
        }
        
        foreach (var (localTransform, damageDigit, entity) in SystemAPI
                     .Query<RefRW<LocalTransform>, RefRO<DamageDigit>>().WithDisabled<DamageDigit>().WithEntityAccess())
        {
            Entity parentEntity = GetRootParent(entity, parentLookup);

            LocalTransform parentTransform;
            if (parentEntity == entity)
            {
                parentTransform = localTransform.ValueRO;
            }
            else
            {
                parentTransform = transformLookup[parentEntity];
            }

            localTransform.ValueRW.Rotation =
                parentTransform.InverseTransformRotation(quaternion.LookRotation(cameraForward, math.up()));
        }
        
    }
    
    Entity GetRootParent(Entity entity, ComponentLookup<Parent> parents)
    {
        Entity current = entity;

        // Walk upward until no Parent exists
        while (parents.HasComponent(current))
        {
            var parent = parents[current].Value;
            current = parent;
        }

        return current;
    }
    
}
