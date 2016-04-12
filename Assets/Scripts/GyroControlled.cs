using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GyroControlled : MonoBehaviour
{
    
    private Quaternion initialRotation;
    private Quaternion gyroInitialRotation;
    
    void Start()
    {
        Input.gyro.enabled = true;
        initialRotation = transform.rotation; 
    }
    
    void Update()
    {
        UpdateView();
    }
    
    private void UpdateView()
    {
        ConsoleController.Log(Input.gyro.rotationRate);
        transform.Rotate(Input.gyro.rotationRate);
        return;
        Quaternion offsetRotation = gyroInitialRotation * Input.gyro.attitude;
        offsetRotation.z *= -1;
        offsetRotation.w *= -1;
        transform.rotation = initialRotation * offsetRotation;
    }
    
    public void Calibrate()
    {
        var rotation = Quaternion.Inverse(Input.gyro.attitude);
        print(String.Format("Calibrated GyroControlled: {0}", rotation));
        gyroInitialRotation = rotation;
    }
    
}
