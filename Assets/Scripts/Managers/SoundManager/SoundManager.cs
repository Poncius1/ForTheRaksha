using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.PlayOneShot(clip);
    }


    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume= value;
    }
    public void ChangeSFXVolume(float value)
    {
        _effectSource.volume = value;
    }
    public void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }


    public float GetSFXVolume()
    {
      return _effectSource.volume;
    }

    public float GetMusicVolume()
    {
        return _musicSource.volume;
    }

    public float GetMasterVolume()
    {
        return AudioListener.volume;
    }
}
