using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GyroControlled : MonoBehaviour {
    
    private Quaternion initialRotation;
    private Quaternion gyroInitialRotation;
	private bool locked = true;
	private Vector3 lastPosition;
	private bool ena;

    void Start() {
        Input.gyro.enabled = true;
		initialRotation = transform.rotation;
    }

    void Update() {
        UpdateView();
    }

    private void UpdateView() {
		var offsetRotation = gyroInitialRotation * Input.gyro.attitude;
        offsetRotation.z *= -1;
        offsetRotation.w *= -1;
		transform.rotation = initialRotation * offsetRotation;
    }

    public void Calibrate() {
		gyroInitialRotation = Quaternion.Inverse(Input.gyro.attitude);
    }

	public void Lock() {
		ena = enabled;
		if (!ena)
			return;
		enabled = false;
		lastPosition = transform.position;
	}

	public void Release() {
		if (!ena)
			return;
		enabled = true;
		transform.position = lastPosition;
	}

}
