using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MobTargetSetter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targetGameObjects;
    private List<Targetable> _targetables = new List<Targetable>();

    private EntityManager _entityManager;
    private MobTarget _mobTarget;
    private Entity _targetsEntity;

    private void Awake()
    {
        foreach (var go in _targetGameObjects)
        {
            if (go.TryGetComponent<Targetable>(out var targetable))
            {
                _targetables.Add(targetable);
            }
        }
    }

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        _targetsEntity = _entityManager.CreateEntity();
        DynamicBuffer<MobTarget> buffer = _entityManager.AddBuffer<MobTarget>(_targetsEntity);

        foreach (var targetable in _targetables)
        {
            buffer.Add(new MobTarget
            {
                ID = targetable.ID,
                TargetType = targetable.TargetType,
                Position = float3.zero,
            });
        }
    }

    private void Update()
    {
        DynamicBuffer<MobTarget> mobTargetBuffer = _entityManager.GetBuffer<MobTarget>(_targetsEntity);

        for (int i = 0; i < mobTargetBuffer.Length; i++)
        {
            MobTarget target = mobTargetBuffer[i];

            foreach (var targetable in _targetables)
            {
                if (target.TargetType == targetable.TargetType)
                {
                    target.Position = targetable.transform.position;
                    mobTargetBuffer[i] = target;
                    break;
                }
            }
        }
        
    }
    
}

