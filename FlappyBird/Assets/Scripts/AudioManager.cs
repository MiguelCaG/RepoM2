using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public static AudioManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVol", volume);
    }

    public float GetMusicVolume()
    {
        audioMixer.GetFloat("musicVol", out float vol);
        return vol;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVol", volume);
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat("sfxVol", out float vol);
        return vol;
    }
}
