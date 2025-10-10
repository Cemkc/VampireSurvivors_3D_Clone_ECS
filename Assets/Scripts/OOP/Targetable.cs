using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum DamageableType
{
    None,
    Character,
    Building
}

public abstract class Targetable : MonoBehaviour, IGameRunning
{
    public static Action<Targetable> OnCreated;
    
    private int id;
    [FormerlySerializedAs("_targetType")] [SerializeField] private GameObjectType gameObjectType = GameObjectType.Character1;

    public int ID => id;
    public GameObjectType GameObjectType => gameObjectType;

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
    
    