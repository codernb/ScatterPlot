using UnityEngine;
using System.Collections;

public class AxesController : MonoBehaviour {

	public Transform Roundtripper;

	public float ScaleX {
		set {
			Scale (0, value);
		}
	}

	public float ScaleY {
		set {
			Scale (1, value);
		}
	}

	public float ScaleZ {
		set {
			Scale (2, value);
		}
	}

	private void Scale(byte axe, float value) {
		var scale = transform.localScale;
		var pos = transform.position;
		var tripperPos = Roundtripper.position;
		scale.y = GetScale(value);
		switch (axe) {
		case 0:
			pos.x = GetScale (value);
			tripperPos.x = scale.y / 2;
			break;
		case 1:
			pos.y = GetScale(value);
			tripperPos.y = scale.y / 2;
			break;
		case 2:
			pos.z = GetScale(value);
			tripperPos.z = scale.y / 2;
			break;
		}
		transform.localScale = scale;
		transform.position = pos;
		Roundtripper.position = tripperPos;
	}

	private float GetScale(float value) {
		return value / 2;
	}

	private float GetPos(float value) {
		return value / 2;
	}

}
