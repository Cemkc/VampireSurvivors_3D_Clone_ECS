using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class DamageBridge : MonoBehaviour, IGameRunning
{
    private Dictionary<int, Targetable> _targetables = new();

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
            MobDamageGivenEvent mobDamageGivenEvent = entityManager.GetComponentData<MobDamageGivenEvent>(entities[i]);
            _targetables.TryGetValue(mobDamageGivenEvent.Id, out Targetable targetable);
            if (targetable != null) targetable.TakeDamage(mobDamageGivenEvent.Amount);

            entityManager.DestroyEntity(entities[i]);
        }
        
    }
    
    private void DamageableCreatedCallback(Targetable targetable)
    {
        if (!_targetables.ContainsKey(targetable.ID))
        {
            _targetables.Add(targetable.ID, targetable);
        }
    }
    
}