using UnityEngine;
using System.Collections;

public class SlowYRotation : MonoBehaviour
{

    public float rotationSpeed = 20f;
    

    public float startDelay = 2f;
    

    public float[] rotationStops = new float[] { 180f, 360f };
    

    public float pauseDuration = 1f;
    

    public bool loop = false;
    

    public float arrivalThreshold = 0.5f;
    
    private bool canRotate = false;
    private float accumulatedRotation = 0f;

    void Start()
    {

        accumulatedRotation = transform.localEulerAngles.y;
        StartCoroutine(RotationRoutine());
    }

    IEnumerator RotationRoutine()
    {

        yield return new WaitForSeconds(startDelay);
        canRotate = true;

        do
        {
            for (int i = 0; i < rotationStops.Length; i++)
            {
                float targetY = rotationStops[i];
                

                yield return StartCoroutine(RotateToAngle(targetY));
                

                yield return new WaitForSeconds(pauseDuration);
            }
            
            // when looping, continue from current position (no reset/snap)
            
        } while (loop);
    }

    IEnumerator RotateToAngle(float targetY)
    {
        float startRotation = accumulatedRotation;
        float targetRotation = targetY;
        

        float rotationNeeded = targetRotation - startRotation;
        

        if (rotationNeeded < 0f)
        {
            rotationNeeded += 360f;
        }
        
        float rotationDone = 0f;
        
        while (rotationDone < rotationNeeded)
        {
            float step = rotationSpeed * Time.deltaTime;
            

            if (rotationDone + step > rotationNeeded)
            {
                step = rotationNeeded - rotationDone;
            }
            
            rotationDone += step;
            accumulatedRotation += step;
            
            Vector3 currentRotation = transform.localEulerAngles;
            float newY = accumulatedRotation % 360f;
            transform.localEulerAngles = new Vector3(currentRotation.x, newY, currentRotation.z);
            
            yield return null;
        }
        

        accumulatedRotation = targetRotation;
        Vector3 finalRotation = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(finalRotation.x, targetRotation % 360f, finalRotation.z);
    }
    

    public void StartRotation()
    {
        StopAllCoroutines();
        StartCoroutine(RotationRoutine());
    }
    

    public void StopRotation()
    {
        StopAllCoroutines();
        canRotate = false;
    }
}