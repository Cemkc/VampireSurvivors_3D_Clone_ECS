using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class SetTargetForMobs : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        SetMoveTargetForEntities(entityManager);
        
        if (_targetTransform.TryGetComponent(out Damageable damageable))
        {
            SetHitTargetForEntities(entityManager, damageable);   
        }
        
    }

    private void SetMoveTargetForEntities(EntityManager entityManager)
    {
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<UnitMover>()
            .Build(entityManager);
        
        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Temp);
        for (int i = 0; i < entities.Length; i++)
        {
            Entity entity = entities[i];
            
            UnitMover mover = entityManager.GetComponentData<UnitMover>(entity);
            mover.targetPosition = _targetTransform.position;
            entityManager.SetComponentData(entity, mover);
        }
        
        entities.Dispose();
    }

    private void SetHitTargetForEntities(EntityManager entityManager, Damageable damageable)
    {
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<HitTarget>().Build(entityManager);
        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Temp);
        
        for (int i = 0; i < entities.Length; i++)
        {
            Entity entity = entities[i];
            
            HitTarget hitTarget = entityManager.GetComponentData<HitTarget>(entity);
            hitTarget.TargetID = damageable.ID;
            entityManager.SetComponentData(entity, hitTarget);
        }
        
        entities.Dispose();
    }

    public void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }
    
}

