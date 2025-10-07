using System;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    public const int MOB_LAYER = 6;
    
    public static GameAssets Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
