using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    [SerializeField] private SwitchVCam switchVCam; // Referencia al script SwitchVCam
    [SerializeField] private Slider aimSensitivitySlider;
    [SerializeField] private Slider normalSensitivitySlider;

    private void Start()
    {
        // Aseg�rate de que switchVCam no sea nulo y los sliders est�n asignados
        if (switchVCam != null && aimSensitivitySlider != null && normalSensitivitySlider != null)
        {
            // Configura los valores iniciales de los sliders
            aimSensitivitySlider.value = switchVCam.aimSensivility;
            normalSensitivitySlider.value = switchVCam.normalSensivility;
        }
    }

    public void UpdateAimSensitivity(float value)
    {
        // Actualiza el valor de aimSensivility en SwitchVCam
        switchVCam.aimSensivility = value;
    }

    public void UpdateNormalSensitivity(float value)
    {
        // Actualiza el valor de normalSensivility en SwitchVCam
        switchVCam.normalSensivility = value;
    }
}
