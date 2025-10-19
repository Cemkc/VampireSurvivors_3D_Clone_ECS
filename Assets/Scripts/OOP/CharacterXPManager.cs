using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public struct LevelDefinition
{
    public int LevelNumber;
    public int NeededXPtoComplete;
}

public class CharacterXPManager : MonoBehaviour
{
    [SerializeField] private XPSettings m_XPSettingsAsset;
    private XPSettings m_XPSettings;
    
    private int m_currentXP;
    private int m_characterLevel;

    private Dictionary<int, LevelDefinition> m_levelDefinitions = new();
    
    private void Awake()
    {
        m_XPSettings = Instantiate(m_XPSettingsAsset);

        int neededXP = m_XPSettings.FirstLevelNeededXP;
        for (int i = 0; i < m_XPSettings.MaxNumberOfLevels; i++)
        {
            LevelDefinition levelDefinition = new LevelDefinition
            {
                LevelNumber = i + 1,
                NeededXPtoComplete = neededXP,
            };

            m_levelDefinitions.TryAdd(i + 1, levelDefinition);

            neededXP += neededXP * m_XPSettings.XPIncrementMultiplier;
        }

        m_characterLevel = 1;
    }

    private void FixedUpdate()
    {
        m_characterLevel = m_currentXP;
    }

    public void GainXP(int amount)
    {
        m_currentXP += (int)(amount * m_XPSettings.XpGainMultiplier);
    }
}
