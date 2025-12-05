using UnityEngine;

public enum SoundLabel
{
    InGameMusic,
    MainMenuMusic,
    MobDeathSound,
    DigitExplosionSound,
    LevelUpSound,
    PlayerDeathSound,
    MobGiveDamageSound,
}

[System.Serializable]
public class Sound
{
    public SoundLabel label;
    public AudioClip clip;

    [Range(0f, 1f)] 
    public float volume = 0.7f;
    
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
