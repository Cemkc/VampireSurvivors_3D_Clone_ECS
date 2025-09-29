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
        EntityQuery entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<UnitMover>().Build(entityManager);

        NativeArray<UnitMover> unitMoverArray = entityQuery.ToComponentDataArray<UnitMover>(Allocator.Temp);
        for (int i = 0; i < unitMoverArray.Length; i++)
        {
            UnitMover unitMover = unitMoverArray[i];
            unitMover.targetPosition = CharLogic.transform.position;
            unitMoverArray[i] = unitMover;
        }
    }
}

