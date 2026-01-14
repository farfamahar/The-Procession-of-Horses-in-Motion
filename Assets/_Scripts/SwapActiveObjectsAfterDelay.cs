using UnityEngine;
using System.Collections;

public class SwapActiveObjectsAfterDelay : MonoBehaviour
{

    public GameObject objectToDeactivate;
    public GameObject objectToActivate;


    public float delaySeconds = 2f;


    public bool useUnscaledTime = false;

    void Start()
    {
        StartCoroutine(SwapRoutine());
    }

    IEnumerator SwapRoutine()
    {
        if (delaySeconds > 0f)
        {
            if (useUnscaledTime)
                yield return new WaitForSecondsRealtime(delaySeconds);
            else
                yield return new WaitForSeconds(delaySeconds);
        }

        if (objectToDeactivate != null)
            objectToDeactivate.SetActive(false);

        if (objectToActivate != null)
            objectToActivate.SetActive(true);
    }
}
