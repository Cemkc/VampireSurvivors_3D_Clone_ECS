
using Unity.Entities;
using UnityEngine;

public struct CharacterStatsComponent : IComponentData
{
    public float DamageDigitExplosionChance;
}

public class CharacterStatsAuthoring : MonoBehaviour
{
    public CharacterStats CharacterStats;
    
    public class Baker : Baker<CharacterStatsAuthoring>
    {
        public override void Bake(CharacterStatsAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new CharacterStatsComponent
            {
                DamageDigitExplosionChance = authoring.CharacterStats.DamageDigitExplosionChance,
            });
        }
    }
}
    