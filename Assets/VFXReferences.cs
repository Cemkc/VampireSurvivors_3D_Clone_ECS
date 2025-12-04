using System;
using UnityEngine;

public class VFXReferences : MonoBehaviour
{
    public static VFXReferences Instance;

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

    public GameObject DigitExplosionEffect;
}
