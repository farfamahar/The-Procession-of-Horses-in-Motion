using UnityEngine;

public class RandomAudioStart : MonoBehaviour
{

    public AudioClip[] clips;   


    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;


    public float minVolume = 0.8f;
    public float maxVolume = 1.0f;


    [Range(0f, 1f)]
    public float spatialBlend = 1f;
    public float minDistance = 2f;
    public float maxDistance = 25f;
    public float spread = 45f;
    public float dopplerLevel = 0.1f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (clips == null || clips.Length == 0)
            return;


        audioSource.clip = clips[Random.Range(0, clips.Length)];


        audioSource.pitch = Random.Range(minPitch, maxPitch);


        audioSource.volume = Random.Range(minVolume, maxVolume);


        audioSource.spatialBlend = spatialBlend;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.spread = spread;
        audioSource.dopplerLevel = dopplerLevel;


        audioSource.time = Random.Range(0f, audioSource.clip.length);

        audioSource.Play();
    }
}