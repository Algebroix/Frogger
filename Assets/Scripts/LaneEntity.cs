using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneEntity : MonoBehaviour
{
    private Vector2 startingPosition;

    private Rigidbody2D rigidbody2D;

    private int lanePointLayer;

    public void SetVelocity(Vector2 newVelocity)
    {
        rigidbody2D.velocity = newVelocity;
    }

    public void SetStartingPosition(Vector2 newStartingPosition)
    {
        startingPosition = newStartingPosition;
    }

    public void Init()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == lanePointLayer)
        {
            rigidbody2D.position = startingPosition;
        }
    }

    private void Awake()
    {
        lanePointLayer = LayerMask.NameToLayer("LanePoint");
    }
}