using System;
using System.Drawing;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private RectTransform m_XPFillBar;
    private float m_MaxFillAmount;
    private float m_CurrentFillAmount;

    private void Awake()
    {
        m_MaxFillAmount = m_XPFillBar.sizeDelta.x;
        m_CurrentFillAmount = 0;
        
        m_XPFillBar.sizeDelta = new Vector2(0f, m_XPFillBar.sizeDelta.y);
    }

    private void Start()
    {
        if(CharacterXPManager.Instance) CharacterXPManager.Instance.OnXPGain += GainXPCallback;
    }

    private void OnEnable()
    {
        if(CharacterXPManager.Instance) CharacterXPManager.Instance.OnXPGain += GainXPCallback;
    }

    private void OnDisable()
    {
        if(CharacterXPManager.Instance) CharacterXPManager.Instance.OnXPGain -= GainXPCallback;
    }

    public void GainXPCallback(XPGainEventInfo info)
    {
        float percentage = (float)info.CurrentXP / info.NextLevelRequiredXP;
        // Debug.Log($"[XP Debug] CurrentXP: {info.CurrentXP}, NextLevelXP: {info.NextLevelRequiredXP}, Percentage: {percentage:P2}");

        SetFillBar(percentage);
    }

    public void SetFillBar(float percentage)
    {
        float fillAmount = percentage * m_MaxFillAmount;
        m_CurrentFillAmount = Mathf.Clamp(fillAmount, 0, m_MaxFillAmount);

        Vector2 XPBarSizeDelta = m_XPFillBar.sizeDelta;
        m_XPFillBar.sizeDelta = new Vector2(m_CurrentFillAmount, XPBarSizeDelta.y);
    }
}
