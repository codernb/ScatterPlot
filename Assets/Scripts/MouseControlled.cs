using UnityEngine;
using System.Collections;

public class MouseControlled : MonoBehaviour {

	public GyroControlled GyroController;

    private Vector3 rot = Vector3.zero;
    private bool locked = false;

    void Start() {
		#if !UNITY_EDITOR
		enabled = false;
		#else
		GyroController.enabled = false;
		#endif
    }

    void Update() {
        if (Input.GetButtonDown("Jump"))
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