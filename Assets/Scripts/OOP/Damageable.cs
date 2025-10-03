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
    public static Action<Damageable> OnCreated;
    
    private int id;
    protected int _health;

    public int ID => id;

    private void Awake()
    {
        OnAwake();
    }

    private void Update()
    {
        OnUpdate();
    }
    
    #region "MonoBehaviour virtuals" 
    protected virtual void OnAwake()
    {
        id = GetInstanceID();
        OnCreated?.Invoke(this);
    }

    protected virtual void OnUpdate(){ }
    
    #endregion

    public abstract void TakeDamage(int damageAmount);

}
    
    