using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float updateInterval = 0.5f;

    private float accum = 0.0f;
    private int frames = 0;
    private float timeLeft;

    private void Start()
    {
        timeLeft = updateInterval;

        // Asegúrate de que fpsText esté asignado en el inspector.
        if (fpsText == null)
        {
            Debug.LogError("El objeto de texto para mostrar FPS no está asignado.");
            enabled = false; // Deshabilita este script para evitar errores.
            return;
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (timeLeft <= 0.0)
        {
            float fps = accum / frames;
            fpsText.text = string.Format("{0:F2} FPS", fps);

            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
