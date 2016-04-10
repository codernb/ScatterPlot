using UnityEngine;
using System.Collections;

public class BillboardController : MonoBehaviour
{

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
