using UnityEngine;
using System.Collections;

public class CompassController : MonoBehaviour
{

    void Update()
    {
        transform.rotation = Quaternion.Inverse(Camera.main.transform.rotation);
    }
}
