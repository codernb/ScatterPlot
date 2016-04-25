using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public void Release() {
		transform.parent = null;
	}

}
