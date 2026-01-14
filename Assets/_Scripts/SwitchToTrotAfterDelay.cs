using UnityEngine;
using System.Collections;

public class SwitchToTrotAfterDelay : MonoBehaviour
{

    public Animator animator;


    public float delaySeconds = 3f;


    public HorseCleanup horseCleanup; 


    public bool useTrigger = true;

    public string trotTriggerName = "Trot";


    public string trotStateName = "Trot";

    public float crossFadeDuration = 0.2f;
    public int layer = 0;

    void Reset()
    {
        animator = GetComponent<Animator>();
        horseCleanup = GetComponent<HorseCleanup>();
    }

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (horseCleanup == null) horseCleanup = GetComponent<HorseCleanup>();
    }

    void Start()
    {
        StartCoroutine(SwitchRoutine());
    }

    IEnumerator SwitchRoutine()
    {
        yield return new WaitForSeconds(delaySeconds);


        if (horseCleanup != null)
            horseCleanup.CancelCleanup();

        if (animator == null) yield break;

        if (useTrigger)
        {
            animator.ResetTrigger(trotTriggerName);
            animator.SetTrigger(trotTriggerName);
        }
        else
        {
            animator.CrossFade(trotStateName, crossFadeDuration, layer);
        }
    }
}
