using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Settings-Configs/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public float MoveSpeed;

    public int Health;

    [Range(0f, 1f)]
    public float DamageDigitExplosionChance;
}

[CreateAssetMenu(fileName = "XPSettings", menuName = "Settings-Configs/CharacterStats")]
public class XPSettings : ScriptableObject
{
    [Header("XP gain definitions")] 
    public float XpGainMultiplier;
    public int MaxNumberOfLevels;
    public int FirstLevelNeededXP;
    public int XPIncrementMultiplier;
}