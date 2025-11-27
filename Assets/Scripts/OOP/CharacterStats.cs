using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Settings-Configs/CharacterStats")]
public class CharacterStats : ScriptableObject
{
    public float MoveSpeed;

    public int Health;

    [Range(0f, 1f)]
    public float DamageDigitExplosionChance;
}
