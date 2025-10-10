using System;
using UnityEngine;

public class CharacterHurtManager : Targetable, IGameRunning
{
    private CharacterLogic _characterLogic;
    private CharacterStats _characterStats;

    public Action<int> OnDamageTaken;
    public Action<Targetable> OnDeath;
    
    public int Health;

    public override void TakeDamage(int damageAmount)
    {
        if (Health <= 0)
        {
            // TO DO: die.
            OnDeath?.Invoke(this);
        }
        
        OnDamageTaken?.Invoke(damageAmount);
    }
}
    