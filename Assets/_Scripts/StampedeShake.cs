using UnityEngine;
using Unity.Cinemachine;

public class StampedeShake : MonoBehaviour
{

    public CinemachineCamera cinemachineCamera;
    

    public float buildupDuration = 10f;
    

    public float maxAmplitude = 3f;
    

    public float maxFrequency = 2f;
    

    public AnimationCurve intensityCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    

    public bool autoStart = false;
    
    private CinemachineBasicMultiChannelPerlin perlinNoise;
    private float currentTime = 0f;
    private bool isShaking = false;
    private float startAmplitude = 0f;
    private float startFrequency = 0f;

    void Start()
    {

        if (cinemachineCamera == null)
        {
            cinemachineCamera = GetComponent<CinemachineCamera>();
        }
        

        if (cinemachineCamera != null)
        {
            perlinNoise = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            
            if (perlinNoise == null)
            {
                Debug.LogError("no CinemachineBasicMultiChannelPerlin");
            }
            else
            {

                startAmplitude = perlinNoise.AmplitudeGain;
                startFrequency = perlinNoise.FrequencyGain;
            }
        }
        
        if (autoStart)
        {
            StartStampede();
        }
    }

    void Update()
    {
        if (isShaking && perlinNoise != null)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / buildupDuration);
            

            float curvedProgress = intensityCurve.Evaluate(progress);
            

            perlinNoise.AmplitudeGain = Mathf.Lerp(startAmplitude, maxAmplitude, curvedProgress);
            perlinNoise.FrequencyGain = Mathf.Lerp(startFrequency, maxFrequency, curvedProgress);
            
    
            if (progress >= 1f)
            {
                isShaking = false;
            }
        }
    }
    

    public void StartStampede()
    {
        if (perlinNoise != null)
        {
            currentTime = 0f;
            isShaking = true;
            

            perlinNoise.AmplitudeGain = startAmplitude;
            perlinNoise.FrequencyGain = startFrequency;
        }
    }
    

    public void StopStampede()
    {
        isShaking = false;
        if (perlinNoise != null)
        {
            perlinNoise.AmplitudeGain = startAmplitude;
            perlinNoise.FrequencyGain = startFrequency;
        }
    }
    

    public void ResetStampede()
    {
        currentTime = 0f;
        isShaking = false;
        if (perlinNoise != null)
        {
            perlinNoise.AmplitudeGain = startAmplitude;
            perlinNoise.FrequencyGain = startFrequency;
        }
    }
}