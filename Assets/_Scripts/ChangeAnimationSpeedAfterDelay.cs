using UnityEngine;
using System.Collections;

public class ChangeAnimationSpeedAfterDelay : MonoBehaviour
{
    public Animator animator;

    public float delaySeconds = 2f;

    public float rampDuration = 2f;

    public float targetSpeed = 0.2f;

    void Reset()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(RampRoutine());
    }

    IEnumerator RampRoutine()
    {
        yield return new WaitForSeconds(delaySeconds);

        if (animator == null) yield break;

        float startSpeed = animator.speed;

        if (rampDuration <= 0f)
        {
            animator.speed = targetSpeed;
            yield break;
        }

        float t = 0f;
        while (t < rampDuration)
        {
            t += Time.deltaTime;
            float u = Mathf.Clamp01(t / rampDuration);

            animator.speed = Mathf.Lerp(startSpeed, targetSpeed, u);
            yield return null;
        }

        animator.speed = targetSpeed;
    }
}
