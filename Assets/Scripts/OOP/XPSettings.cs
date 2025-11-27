using UnityEngine;

[CreateAssetMenu(fileName = "XPSettings", menuName = "Settings-Configs/XPSettings")]
public class XPSettings : ScriptableObject
{
    [Header("XP gain definitions")] 
    public int FirstLevelNeededXP;
    public float XpGainMultiplier;
    public int MaxNumberOfLevels;
    public int XPIncrementMultiplier;
}
