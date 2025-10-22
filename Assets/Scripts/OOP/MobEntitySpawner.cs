using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MobEntitySpawner : MonoBehaviour, IGameRunning
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
        if (PlayerInput.Instance.InputActions.Player.Jump.WasPerformedThisFrame())
        {
            for (int i = 0; i < 1000; i++)
            {
                Entity newMob = _entityManager.Instantiate(_entityReferences.MobPrefabEntity);
                float3 position = new float3
                {
                    x = transform.localPosition.x + math.floor(i / 10) * 3,
                    y = transform.localPosition.y,
                    z = transform.localPosition.z + (i % 10) * 3,
                };
                _entityManager.SetComponentData(newMob, new LocalTransform
                {
                    Position = position,
                    Rotation = quaternion.identity,
                    Scale = 1,
                });
                
                UnitMover unitMover = _entityManager.GetComponentData<UnitMover>(newMob);
                unitMover.targetPosition = position;
                _entityManager.SetComponentData(newMob, unitMover);
            }
        }

    }

    public void OnStateEnable()
    {
        enabled = true;
    }

    public void OnStateDisable()
    {
        enabled = false;
    }
}
    