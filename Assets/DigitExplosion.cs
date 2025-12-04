using UnityEngine;

public class DigitExplosion : MonoBehaviour
{
    public ParticleSystem ps;

    void Update()
    {
        // Just watching until everything (children included) finishes
        if (!ps.IsAlive(true))
        {
            Destroy(gameObject);
        }
    }
}
