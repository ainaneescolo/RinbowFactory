using UnityEngine;
using UnityEngine.Audio;

public class managerSound : MonoBehaviour
{
    public AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Ajustar el volumen de la m�sica
    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
    }

    // Ajustar el volumen de los sonidos
    public void SetSoundVolume(float volume)
    {
        audioManager.SetSoundVolume(volume);
    }
}