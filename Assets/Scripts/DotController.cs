using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour
{

    public float[] Values;

    public void SetAxis(int axis, int i)
    {
        var temp = transform.position;
        switch (axis) {
        case 0:
            temp.x = Values[i];
            break;
        case 1:
            temp.y = Values[i];
            break;
        case 2:
            temp.z = Values[i];
            break;
        }
        transform.position = temp;
    }

}
