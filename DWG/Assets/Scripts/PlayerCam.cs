using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {

	public Transform target;
	float distance = 10.0f;

	float xSpeed = 250.0f;
	float ySpeed = 120.0f;

	float yMinLimit = -20f;
	float yMaxLimit = 80f;

	private float x = 0.0f;
	private float y = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>()){
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		if (target) {
			x += Input.GetAxis("HorizCam"+target.gameObject.GetComponent<PlayerController>().playerNum.ToString()) * xSpeed * 0.02f;
			y -= Input.GetAxis("VertCam"+target.gameObject.GetComponent<PlayerController>().playerNum.ToString()) * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

			transform.rotation = rotation;
			transform.position = position;
		}

	
	}

	float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}

}
