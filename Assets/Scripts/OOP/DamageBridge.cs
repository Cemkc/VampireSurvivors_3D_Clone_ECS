using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DamageBridge : MonoBehaviour
{
    private Dictionary<int, Targetable> _damageables = new();

    private void Awake()
    {
        Targetable.OnCreated += DamageableCreatedCallback;
    }

    private void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(typeof(DamageEvent));
        NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp);
        
        for (int i = 0; i < entities.Length; i++)
        {
            Debug.Log("Found entity with component Damage Event");
            
            DamageEvent damageEvent = entityManager.GetComponentData<DamageEvent>(entities[i]);
            _damageables[damageEvent.id].TakeDamage(damageEvent.amount);
            
            entityManager.DestroyEntity(entities[i]);
        }
        
    }
    
    private void DamageableCreatedCallback(Targetable targetable)
    {
        if (!_damageables.ContainsKey(targetable.ID))
        {
            _damageables.Add(targetable.ID, targetable);
        }
    }
    
}