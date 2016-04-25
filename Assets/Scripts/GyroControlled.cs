using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GyroControlled : MonoBehaviour {
    
    private Quaternion initialRotation;
    private Quaternion gyroInitialRotation;

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
}
