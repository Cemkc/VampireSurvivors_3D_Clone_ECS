using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IGamePlayerPause
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnVolumeChanged(float value)
    {
        AudioManager.Instance.SetGlobalVolume(value);
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
