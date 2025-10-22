using UnityEngine;

public class GiveXP : MonoBehaviour
{
    public int xpAmount;
    
    public void GiveXPToCharacter()
    {
        CharacterXPManager.Instance.GainXP(xpAmount);
    }
}
