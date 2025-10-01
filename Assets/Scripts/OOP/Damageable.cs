using System;
using UnityEngine;

public enum DamageableType
{
    None,
    Character,
    Building
}

public abstract class Damageable : MonoBehaviour
{
    private int id;
    protected int _health;

    public int ID => id;

    private void Awake()
    {
        id = gameObject.GetInstanceID();
    }

    public abstract void TakeDamage(int damageAmount);

}
    
    