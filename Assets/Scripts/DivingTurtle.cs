using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingTurtle : MonoBehaviour
{
    [SerializeField]
    private float aboveTime = 1.0f;

    private Animator animator;

    private int walkableLayer;
    private int obstacleLayer;

    private float currentAboveTime = 0;

    private int diveHash;

    private void Dive()
    {
        currentAboveTime = -1.0f;
        gameObject.layer = obstacleLayer;
    }

    private void Emerge()
    {
        gameObject.layer = walkableLayer;
        currentAboveTime = 0.0f;
    }

    private void Update()
    {
        if (currentAboveTime > aboveTime)
        {
            animator.Play(diveHash);
        }
        else if (currentAboveTime >= 0.0f)
        {
            currentAboveTime += Time.deltaTime;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        walkableLayer = LayerMask.NameToLayer("Walkable");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        diveHash = Animator.StringToHash("Dive");
    }
}