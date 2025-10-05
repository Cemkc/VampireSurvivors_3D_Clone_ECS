using System;
using UnityEngine;

public enum DamageableType
{
    None,
    Character,
    Building
}

public abstract class Targetable : MonoBehaviour
{
    public static Action<Targetable> OnCreated;
    
    private int id;
    [SerializeField] private TargetType _targetType = TargetType.Character1; 
    protected int _health;

    public int ID => id;
    public TargetType TargetType => _targetType;

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
    
    