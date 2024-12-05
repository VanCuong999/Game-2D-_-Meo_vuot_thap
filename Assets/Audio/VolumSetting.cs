
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    private void Start() 
    {
        if (!PlayerPrefs.HasKey("musicVolum"))
        {
            PlayerPrefs.SetFloat("musicVolum",1);
        }
        else
        {
            Load();
        }
    }
    public void ChaneVolume()
    {
        AudioListener.volume = musicSlider.value;
    }
    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolum");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolum",musicSlider.value);
    }
}
