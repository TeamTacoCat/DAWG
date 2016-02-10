using UnityEngine;
using System.Collections;

public class SigilPosRandomizer : MonoBehaviour {

	// Use this for initialization
	void Start () {

		transform.localPosition = new Vector3 (Random.Range (-10, 11), Random.Range (0, 11), Random.Range (-10, 11));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
