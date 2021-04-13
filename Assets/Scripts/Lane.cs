using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private Vector2 velocity;

    private LaneEntity[] entities;

    private Vector2 startingPosition;

    private void Init()
    {
        foreach (LaneEntity entity in entities)
        {
            entity.Init();
            entity.SetVelocity(velocity);
            entity.SetStartingPosition(startingPosition);
        }
    }

    private void Awake()
    {
        entities = GetComponentsInChildren<LaneEntity>();
        startingPosition = transform.GetChild(0).position;
        Init();
    }
}