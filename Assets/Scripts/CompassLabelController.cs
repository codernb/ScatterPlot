using UnityEngine;
using System.Collections;

public class CompassLabelController : MonoBehaviour {

    public Transform Cam;

    void Update() {
        transform.localRotation = Cam.rotation;
    }
}
