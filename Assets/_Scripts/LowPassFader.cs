using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class LowPassFader : MonoBehaviour
{

    public float targetCutoff = 500f;      
    public float fadeDuration = 2f;         
    public float delayBeforeFade = 1f;      

    private AudioLowPassFilter lowPass;
    private Coroutine fadeRoutine;

    void Awake()
    {
        lowPass = GetComponent<AudioLowPassFilter>();
    }

    void Start()
    {
        StartFade();
    }

    public void StartFade()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeLowPass());
    }

    private IEnumerator FadeLowPass()
    {
        if (delayBeforeFade > 0f)
            yield return new WaitForSeconds(delayBeforeFade);

        float startValue = lowPass.cutoffFrequency;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            lowPass.cutoffFrequency = Mathf.Lerp(startValue, targetCutoff, t);

            yield return null;
        }

        lowPass.cutoffFrequency = targetCutoff;
    }
}