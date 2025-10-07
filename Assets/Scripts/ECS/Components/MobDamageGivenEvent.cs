using Unity.Entities;

public struct MobDamageGivenEvent : IComponentData
{
    public int Id;
    public int Amount;
}

public struct MobDamageTakenEvent : IComponentData
{
    public int Id;
    public Entity Entity;
    public int Amount;
}
