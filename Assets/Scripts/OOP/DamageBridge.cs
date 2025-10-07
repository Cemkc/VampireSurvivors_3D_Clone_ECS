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
        EntityQuery query = entityManager.CreateEntityQuery(typeof(MobDamageGivenEvent));
        NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp);
        
        for (int i = 0; i < entities.Length; i++)
        {
            Debug.Log("Found entity with component Damage Event");
            
            MobDamageGivenEvent mobDamageGivenEvent = entityManager.GetComponentData<MobDamageGivenEvent>(entities[i]);
            _damageables[mobDamageGivenEvent.Id].TakeDamage(mobDamageGivenEvent.Amount);
            
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