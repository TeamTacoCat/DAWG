using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	public bool ground = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){

			ground = true;

	}

	void OnTriggerStay(Collider coll){

		ground = true;

	}

	void OnTriggerExit(Collider coll){

		ground = false;

	}

}
