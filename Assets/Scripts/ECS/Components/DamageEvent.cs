using Unity.Entities;

public struct DamageEvent : IComponentData
{
    public int id;
    public int amount;
}

