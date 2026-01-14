using UnityEngine;
using System.Collections;

public class DeactivateAfterTime : MonoBehaviour
{
    public float delay = 3f;

    void OnEnable()
    {
        StartCoroutine(DeactivateRoutine());
    }

    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}