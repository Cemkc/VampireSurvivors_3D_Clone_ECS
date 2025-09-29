// using Unity.Entities;
// using Unity.Mathematics;
// using UnityEngine;
//
// public struct FollowPlayer : IComponentData
// {
//     public CharacterController characterController;
// }
//
// public class FollowPlayerAuthoring : MonoBehaviour
// {
//     public CharacterController characterController;
//     
//     public class Baker : Baker<FollowPlayerAuthoring>
//     {
//         public override void Bake(FollowPlayerAuthoring authoring) {
//             Entity entity = GetEntity(TransformUsageFlags.Dynamic);
//             AddComponent(entity, new UnitMover {
//             });
//         }
//     }
// }