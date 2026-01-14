using UnityEngine;

public class RandomAnimationOffset : MonoBehaviour
{

    public string stateName = "Idle";


    public int layer = 0;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        float offset = Random.value;

        animator.Play(stateName, layer, offset);

        animator.Update(0f);
    }
}