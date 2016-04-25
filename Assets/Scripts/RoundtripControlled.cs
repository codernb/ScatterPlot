using UnityEngine;
using System.Collections;

public class RoundtripControlled : MonoBehaviour {

	public float Speed = 1;

	private Vector3 rotation = new Vector3 (0, 0, 0);

	void Start() {
		rotation.y = Speed;
	}

	void Update () {
		transform.Rotate (rotation);
	}

}
