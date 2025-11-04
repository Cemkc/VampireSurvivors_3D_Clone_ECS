using UnityEngine;
using UnityEngine.UI;

public enum UpgradeTypes
{
    None,
    Speed,
    Weapon,
    DigitBomb,
}

public abstract class CharUpgrade : ScriptableObject
{
    [SerializeField] private Texture m_Texture;
    [SerializeField] private string m_Description;
    
    public Texture Texture => m_Texture;
    public string Description => m_Description;

    public abstract void Init();
    public abstract void ApplyUpgrade();
    public abstract UpgradeTypes GetUpgradeType();
}
    