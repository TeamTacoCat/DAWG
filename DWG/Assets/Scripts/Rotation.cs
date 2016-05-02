using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	[SerializeField]float xSpeed = 0;
	[SerializeField]float ySpeed = 0;
	[SerializeField]float zSpeed = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		transform.Rotate (new Vector3 (xSpeed*Time.deltaTime, ySpeed*Time.deltaTime, zSpeed*Time.deltaTime));

	}
}
