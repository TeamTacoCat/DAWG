using UnityEngine;
using System.Collections;

public class PolyCount : MonoBehaviour {

	// Use this for initialization
	void Start () {

		print (gameObject.name + ":  " + gameObject.GetComponent<MeshFilter> ().mesh.triangles.Length / 3);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
