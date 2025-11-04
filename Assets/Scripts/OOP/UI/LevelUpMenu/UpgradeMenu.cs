using System;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeMenu : MonoBehaviour, IGameLevelUp
{
    [SerializeField] private GameObject m_UpgradeVerticalLayoutGroup;

    private void Awake()
    {
        Debug.Log("Subscribing to the event!");
        LevelUpManager.Instance.OnUpgradesAssigned += UpgradesAssignedCallback;
    }

    private void OnDestroy()
    {
        LevelUpManager.Instance.OnUpgradesAssigned -= UpgradesAssignedCallback;
    }

    private void UpgradesAssignedCallback(List<CharUpgrade> upgrades)
    {
        Debug.Log("Callback has heard!");
        for (int i = 0; i < upgrades.Count; i++)
        {
            var upgradePanelTransform = m_UpgradeVerticalLayoutGroup.transform.GetChild(i);
            if (upgradePanelTransform.TryGetComponent(out UpgradePanel panel))
            {
                Debug.Log("Setting a panel for the upgrade " + upgrades[i].Description);
                panel.SetUpgrade(upgrades[i]);
            }
        }
    }

    public void OnStateEnable()
    {
        gameObject.SetActive(true);
        
    }

    public void OnStateDisable()
    {
        gameObject.SetActive(false);
        
    }
}
