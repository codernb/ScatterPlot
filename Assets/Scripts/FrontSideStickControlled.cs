﻿using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class FrontSideStickControlled : MonoBehaviour
{

    public TransformGesture TransformGestureScript;
    public float MoveSpeed = 0.1f;
    public float MaxSpeed = 1;

    private bool Moving;
    private Vector2 StartPosition;

    void Start()
    {
        TransformGestureScript.TransformStarted += delegate {
            Moving = true;
            StartPosition = TransformGestureScript.ScreenPosition;
        };
        TransformGestureScript.TransformCompleted += delegate {
            Moving = false;
        };
    }

    void Update()
    {
        if (!Moving)
            return;
        var diff = (TransformGestureScript.ScreenPosition - StartPosition) * MoveSpeed;
        var position = transform.position;
        var diffX = diff.x > MaxSpeed ? MaxSpeed : diff.x < -MaxSpeed ? -MaxSpeed : diff.x;
        var diffY = diff.y > MaxSpeed ? MaxSpeed : diff.y < -MaxSpeed ? -MaxSpeed : diff.y;
        position += transform.right * diffX + transform.forward * diffY;
        transform.position = position;
    }
	
}
