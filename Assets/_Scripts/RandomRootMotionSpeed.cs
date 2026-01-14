using UnityEngine;

public class RandomRootMotionSpeed : MonoBehaviour
{

    public float minSpeed = 0.8f;


    public float maxSpeed = 1.2f;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        animator.speed = randomSpeed;
    }
}