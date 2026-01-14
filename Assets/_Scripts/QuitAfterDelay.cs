using UnityEngine;
using System.Collections;

public class QuitAfterDelay : MonoBehaviour
{
    public float delaySeconds = 5f;

    public bool useUnscaledTime = false;

    void Start()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        if (delaySeconds > 0f)
        {
            if (useUnscaledTime)
                yield return new WaitForSecondsRealtime(delaySeconds);
            else
                yield return new WaitForSeconds(delaySeconds);
        }

        Application.Quit();
    }
}
