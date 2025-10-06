using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MobEntitySpawner : MonoBehaviour
{
    private EntityManager _entityManager;
    private Entity _entityReferencesEntity;
    private EntityReferences _entityReferences;

    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        _entityReferencesEntity = _entityManager.CreateEntityQuery(typeof(EntityReferences)).GetSingletonEntity();
        _entityReferences = _entityManager.GetComponentData<EntityReferences>(_entityReferencesEntity);
    }

    private void Update()
    {
        if (CharacterInput.Instance.InputActions.Player.Jump.WasPerformedThisFrame())
        {
            for (int i = 0; i < 1000; i++)
            {
                Entity newMob = _entityManager.Instantiate(_entityReferences.MobPrefabEntity);
                float3 position = new float3
                {
                    x = transform.localPosition.x + math.floor(i / 10),
                    y = transform.localPosition.y,
                    z = transform.localPosition.z + i % 10,
                };
                _entityManager.SetComponentData(newMob, new LocalTransform
                {
                    Position = position,
                    Rotation = quaternion.identity,
                    Scale = 1,
                });
            }
            
            Debug.Log("Spawned new ECS enemy entity from MonoBehaviour!");
        }

    }
}
    