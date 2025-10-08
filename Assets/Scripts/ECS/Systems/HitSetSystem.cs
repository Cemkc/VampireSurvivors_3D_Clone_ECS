// using System.Runtime.CompilerServices;
// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
//
// public static class CollisionMapUtils
// {
//     public static void AddCollision(ref ActiveCollisionPairMap collisionPairMap, int colliderId, int hitId)
//     {
//         if (!collisionPairMap.Map.IsCreated || !collisionPairMap.Map[colliderId].IsCreated)
//         {
//             return;
//         }
//         
//         if (collisionPairMap.Map.ContainsKey(colliderId))
//         {
//             NativeParallelHashSet<int> hitList = collisionPairMap.Map[colliderId];
//
//             if (hitList.Contains(hitId))
//             {
//                 return;
//             }
//
//             hitList.Add(hitId);
//         }
//     }
//
//     public static void Remove()
//     {
//         
//     }
//
//     public static void Get()
//     {
//         
//     }
// }
//
//
// internal partial struct HitSetSystem : ISystem
// {
//     [BurstCompile]
//     public void OnCreate(ref SystemState state)
//     {
//         Entity entity = state.EntityManager.CreateEntity();
//
//         ActiveCollisionPairMap collisionPairMap = new ActiveCollisionPairMap
//         {
//             Map = new NativeParallelHashMap<int, NativeParallelHashSet<int>>(50, Allocator.Persistent),
//         };
//
//         foreach (int key in collisionPairMap.Map.GetKeyArray(Allocator.Temp))
//         {
//             collisionPairMap.Map[key] = new NativeParallelHashSet<int>(1024, Allocator.Persistent);
//         }
//
//         state.EntityManager.AddComponentData(entity, collisionPairMap);
//     }
//     
//     [BurstCompile]
//     public void OnDestroy(ref SystemState state)
//     {
//         if (SystemAPI.TryGetSingleton(out ActiveCollisionPairMap collisionPairSet))
//         {
//             //collisionPairSet.Set.Dispose();   
//         }
//     }
// }
