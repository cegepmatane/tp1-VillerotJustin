using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    
    [Header("Mouvment var")]
    private int Direction;
    private Vector3 startPosition;
    [SerializeField] private int startDirection;
    [SerializeField] private int MovementEnmplitude = 25;

    private void Awake() {
        startPosition = transform.position;
        Direction = startDirection % 4;
    }

    private void Update() {
        if (Math.Abs(transform.position.x  - startPosition.x) - MovementEnmplitude > 0 || Math.Abs(transform.position.y  - startPosition.y) - MovementEnmplitude > 0) {
            Direction = (Direction + 2) % 4;
        }
    }

    void FixedUpdate()
    {
        switch (Direction) {
            default:
                // don't move
                break;
            case 0:
                transform.position += Vector3.up/4;
                break;
            case 1:
                transform.position += Vector3.right/4;
                break;
            case 2:
                transform.position += Vector3.down/4;
                break;
            case 3:
                transform.position += Vector3.left/4;
                break;
        }
    }
}
