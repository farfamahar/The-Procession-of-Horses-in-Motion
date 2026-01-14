using UnityEngine;
using System.Collections;

public class HorseCleanup : MonoBehaviour
{
    public float lifetime = 20f;

    Coroutine destroyRoutine;

    void OnEnable()
    {
        StartCleanup();
    }

    void OnDisable()
    {
        CancelCleanup();
    }


    public void StartCleanup()
    {
        CancelCleanup();
        destroyRoutine = StartCoroutine(DestroyAfterDelay(lifetime));
    }


    public void CancelCleanup()
    {
        if (destroyRoutine != null)
        {
            StopCoroutine(destroyRoutine);
            destroyRoutine = null;
        }
    }


    public void StartCleanup(float newLifetime)
    {
        lifetime = newLifetime;
        StartCleanup();
    }

    IEnumerator DestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
