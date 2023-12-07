using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSoundType : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField] private SoundType _soundType;

    public enum SoundType
    {
        SFX,
        Music
        // You can add more volume types as needed
    }



    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        float volume = GetVolume();
        m_AudioSource.volume = volume;
    }

    private float GetVolume()
    {
        float volume = 0f;

        switch (_soundType)
        {
            case SoundType.SFX:
                volume = SoundManager.Instance.GetSFXVolume();
                break;
            case SoundType.Music:
                volume = SoundManager.Instance.GetMusicVolume();
                break;
            default:
                break;
        }

        return volume;
    }
}
