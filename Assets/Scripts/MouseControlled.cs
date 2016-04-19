using UnityEngine;
using System.Collections;

public class MouseControlled : MonoBehaviour {

    private Vector3 rot = Vector3.zero;
    private bool locked = false;

    void Start() {
    }

    void Update() {
        if (Input.GetButtonDown("Fire1"))
            toggleCursorLock();
        if (!locked)
            return;
        rot.x = Input.GetAxis("Mouse Y");
        rot.y = Input.GetAxis("Mouse X");
        transform.Rotate(rot);
    }

    private void toggleCursorLock() {
        if (locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        locked = !locked;
    }
}