using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string musicVolumeParameterName = "Music";
    public string soundVolumeParameterName = "Sound";
    public Slider SoundSlider;
    public Slider MusicSlider;

    private void Start()
    {
        // Cargar configuraci�n de PlayerPrefs
        if (PlayerPrefs.HasKey("Music"))
        {
            
            float musicVolume = PlayerPrefs.GetFloat("Music");
            SetMusicVolume(musicVolume);
        }
        if (PlayerPrefs.HasKey("Sound"))
        {
            float soundVolume = PlayerPrefs.GetFloat("Sound");
            SetSoundVolume(soundVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(musicVolumeParameterName, Mathf.Log10(volume) * 20);
        MusicSlider.value = volume;
        PlayerPrefs.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat(soundVolumeParameterName, Mathf.Log10(volume) * 20);
        SoundSlider.value = volume;
        PlayerPrefs.SetFloat("Sound", volume);
    }
}
