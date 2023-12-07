
using UnityEngine;
using UnityEngine.UI;


public class VolumenSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private VolumeType _volumeType;

    public enum VolumeType
    {
        Master,
        SFX,
        Music
        // You can add more volume types as needed
    }

    // Start is called before the first frame update
    void Awake()
    {
        
        _slider.onValueChanged.AddListener(val => ChangeVolume(val));

        float initialVolume = GetInitialVolume();
        _slider.value = initialVolume;
    }

    private void ChangeVolume(float value)
    {
        switch (_volumeType)
        {
            case VolumeType.Master:
                SoundManager.Instance.ChangeMasterVolume(value);
                break;
            case VolumeType.SFX:
                SoundManager.Instance.ChangeSFXVolume(value);
                break;
            case VolumeType.Music:
                SoundManager.Instance.ChangeMusicVolume(value);
                break;
            // You can add more cases to handle other volume types
            default:
                break;
        }
    }


    private float GetInitialVolume()
    {
        float initialVolume = 0f;

        switch (_volumeType)
        {
            case VolumeType.Master:
                initialVolume = SoundManager.Instance.GetMasterVolume();
                break;
            case VolumeType.SFX:
                initialVolume = SoundManager.Instance.GetSFXVolume();
                break;
            case VolumeType.Music:
                initialVolume = SoundManager.Instance.GetMusicVolume();
                break;
            // Puedes agregar más casos para manejar otros tipos de volumen
            default:
                break;
        }

        return initialVolume;
    }


}
