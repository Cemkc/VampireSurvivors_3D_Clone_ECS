using Unity.Entities;
using UnityEngine;

public struct DamageDigit : IComponentData, IEnableableComponent
{
    public bool AlwaysFaceCamera; 
    public float ExplosionDelay;
}

public class DamageDigitAuthoring : MonoBehaviour
{
    public bool AlwaysFaceCamera = true; 
    public float ExplosionDelay;
    
    public class Baker : Baker<DamageDigitAuthoring>
    {
        public override void Bake(DamageDigitAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new DamageDigit
            {
                AlwaysFaceCamera = authoring.AlwaysFaceCamera,
                ExplosionDelay = authoring.ExplosionDelay,
            });
        }
    }
}
