using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class FadeOutMasterMixer : MonoBehaviour
{

    public AudioMixer mixer;
    public string exposedParam = "MasterVolume";


    public float fadeDuration = 2f;
    public float targetVolumeDb = -80f;


    public float startDelay = 0f;

    void Start()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {

        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);


        float currentVolume;
        mixer.GetFloat(exposedParam, out currentVolume);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float newVolume = Mathf.Lerp(currentVolume, targetVolumeDb, t);
            mixer.SetFloat(exposedParam, newVolume);

            yield return null;
        }

        mixer.SetFloat(exposedParam, targetVolumeDb);
    }
}