using System.Collections;
using UnityEngine;

public class SkyboxCycle : MonoBehaviour
{
    public Material amanecerSkybox;
    public Material diaSkybox;
    public Material tardeSkybox;
    public Material nocheSkybox;

    public float duracionAmanecer = 10f;
    public float duracionDia = 30f;
    public float duracionTarde = 15f;
    public float duracionNoche = 20f;

    public Color colorAmanecer = new Color(1f, 0.75f, 0.5f, 1f);
    public Color colorDia = Color.white;
    public Color colorTarde = new Color(1f, 0.5f, 0.5f, 1f);
    public Color colorNoche = new Color(0.2f, 0.2f, 0.5f, 1f);

    public float intensidadAmanecer = 1f;
    public float intensidadDia = 1f;
    public float intensidadTarde = 1f;
    public float intensidadNoche = 1f;

    public Light directionalLight;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(LerpSkybox(amanecerSkybox, duracionAmanecer, colorAmanecer, intensidadAmanecer));
            yield return StartCoroutine(LerpSkybox(diaSkybox, duracionDia, colorDia, intensidadDia));
            yield return StartCoroutine(LerpSkybox(tardeSkybox, duracionTarde, colorTarde, intensidadTarde));
            yield return StartCoroutine(LerpSkybox(nocheSkybox, duracionNoche, colorNoche, intensidadNoche));
        }
    }

    private IEnumerator LerpSkybox(Material targetSkybox, float duration, Color targetLightColor, float targetLightIntensity)
    {
        float elapsedTime = 0f;

        Material currentSkybox = RenderSettings.skybox;
        Color currentLightColor = directionalLight.color;
        float currentLightIntensity = directionalLight.intensity;

        Debug.Log($"LerpSkybox: Duration {duration}s, Target Light Color: {targetLightColor}, Target Light Intensity: {targetLightIntensity}");

        while (elapsedTime < duration)
        {
            RenderSettings.skybox.Lerp(currentSkybox, targetSkybox, elapsedTime / duration);
            directionalLight.color = Color.Lerp(currentLightColor, targetLightColor, elapsedTime / duration);
            directionalLight.intensity = Mathf.Lerp(currentLightIntensity, targetLightIntensity, elapsedTime / duration);

            Debug.Log($"LerpSkybox: Elapsed Time: {elapsedTime}s");

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que al final del Lerp, los valores estén exactamente como se espera.
        RenderSettings.skybox = targetSkybox;
        directionalLight.color = targetLightColor;
        directionalLight.intensity = targetLightIntensity;

        Debug.Log("LerpSkybox: Transition Completed");
    }
}
