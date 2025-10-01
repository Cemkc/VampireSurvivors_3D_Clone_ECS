using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DamageBridge : MonoBehaviour
{
    private Dictionary<int, Damageable> _damageables = new();

    private void Awake()
    {
        
    }

    private void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery query = entityManager.CreateEntityQuery(typeof(DamageEvent));
        NativeArray<DamageEvent> damageEvents = query.ToComponentDataArray<DamageEvent>(Allocator.Temp);
        
        foreach (DamageEvent damageEvent in damageEvents)
        {
            Debug.Log("Found entity with component Damage Event");
            _damageables[damageEvent.id].TakeDamage(damageEvent.amount);
        }
        
    }
}