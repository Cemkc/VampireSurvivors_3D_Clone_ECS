using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class SetTargetToCharacter : MonoBehaviour
{
    [SerializeField] private CharacterLogic CharLogic;
    
    private void Update()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<UnitMover>()
            .Build(entityManager);
        
        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.Temp);
        for (int i = 0; i < entities.Length; i++)
        {
            Entity entity = entities[i];
            UnitMover mover = entityManager.GetComponentData<UnitMover>(entity);
            mover.targetPosition = CharLogic.transform.position;
            entityManager.SetComponentData(entity, mover);
        }
        
        entities.Dispose();
        
    }
}

