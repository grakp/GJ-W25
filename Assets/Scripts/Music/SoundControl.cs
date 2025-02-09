using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Slider volumeSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
