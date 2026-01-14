using UnityEngine;
using System.Collections;

public class AudioFadeIn : MonoBehaviour
{

    public float targetVolume = 1f;
    public float fadeDuration = 2f;
    public float startDelay = 1f;
    public int audioPriority = 0;
    
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.volume = 0f;
            audioSource.priority = audioPriority;
        }
    }
    
    void Start()
    {
        if (audioSource == null || audioSource.clip == null)
        {
            Debug.LogError($"AudioSource missing on {gameObject.name}");
            return;
        }
        
        StartCoroutine(FadeInAfterDelay());
    }
    
    IEnumerator FadeInAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        yield return null; 
        
        audioSource.Play();
        
        yield return null;
        if (!audioSource.isPlaying)
        {
            Debug.LogWarning($"Audio didnt play on {gameObject.name}");
        }
        
        float startVolume = 0f;
        float time = 0f;
        
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            yield return null;
        }
        
        audioSource.volume = targetVolume;
    }
}