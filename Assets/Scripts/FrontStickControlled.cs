using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class FrontStickControlled : MonoBehaviour
{
    
    public TransformGesture TransformGestureScript;
    public float MoveSpeed = 0.1f;
    public float MaxSpeed = 1;
	public float DeadZone = 0.2f;
    
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
		var diff = (TransformGestureScript.ScreenPosition - StartPosition);
		if (diff.magnitude < DeadZone)
			return;
		diff *= MoveSpeed;
        var position = transform.position;
        var diffY = diff.y > MaxSpeed ? MaxSpeed : diff.y < -MaxSpeed ? -MaxSpeed : diff.y;
        position += transform.forward * diffY;
        transform.position = position;
    }
}
