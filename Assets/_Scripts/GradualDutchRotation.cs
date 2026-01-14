using UnityEngine;
using Unity.Cinemachine;

public class GradualDutchRotation : MonoBehaviour
{

    public CinemachineCamera cinemachineCamera;
    

    public float rotationDuration = 5f;
    

    public float startDutch = 0f;
    

    public float targetDutch = 360f;
    

    public AnimationCurve rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    

    public float startDelay = 0f;
    

    public bool loopRotation = false;
    
    public bool autoStart = false;
    
    private float currentTime = 0f;
    private float delayTimer = 0f;
    private bool isRotating = false;
    private bool isWaitingForDelay = false;

    void Start()
    {
        // Get the Cinemachine camera if not assigned
        if (cinemachineCamera == null)
        {
            cinemachineCamera = GetComponent<CinemachineCamera>();
        }
        
        if (cinemachineCamera == null)
        {
            Debug.LogError("no CinemachineCamera");
            return;
        }
        
        // Set initial dutch angle
        cinemachineCamera.Lens.Dutch = startDutch;
        
        if (autoStart)
        {
            StartRotation();
        }
    }

    void Update()
    {
        // Handle delay timer
        if (isWaitingForDelay)
        {
            delayTimer += Time.deltaTime;
            
            if (delayTimer >= startDelay)
            {
                isWaitingForDelay = false;
                isRotating = true;
            }
        }
        
        if (isRotating && cinemachineCamera != null)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / rotationDuration);
            

            float curvedProgress = rotationCurve.Evaluate(progress);
            

            cinemachineCamera.Lens.Dutch = Mathf.Lerp(startDutch, targetDutch, curvedProgress);
            

            if (progress >= 1f)
            {
                if (loopRotation)
                {

                    currentTime = 0f;
                }
                else
                {
                    isRotating = false;
                }
            }
        }
    }
    

    public void StartRotation()
    {
        if (cinemachineCamera != null)
        {
            currentTime = 0f;
            delayTimer = 0f;
            cinemachineCamera.Lens.Dutch = startDutch;
            
            if (startDelay > 0f)
            {
                isWaitingForDelay = true;
                isRotating = false;
            }
            else
            {
                isWaitingForDelay = false;
                isRotating = true;
            }
        }
    }
    

    public void StopRotation()
    {
        isRotating = false;
        isWaitingForDelay = false;
    }
    

    public void ResetRotation()
    {
        currentTime = 0f;
        delayTimer = 0f;
        isRotating = false;
        isWaitingForDelay = false;
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Lens.Dutch = startDutch;
        }
    }
    
    public void SetTargetAndStart(float newTarget)
    {
        targetDutch = newTarget;
        StartRotation();
    }
    
    public void ReverseRotation()
    {
        float temp = startDutch;
        startDutch = targetDutch;
        targetDutch = temp;
        StartRotation();
    }
}