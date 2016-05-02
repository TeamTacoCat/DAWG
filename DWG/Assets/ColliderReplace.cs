using UnityEngine;
using System.Collections;

public class ColliderReplace : MonoBehaviour {

	[SerializeField]private PhysicMaterial mat;

	// Use this for initialization
	void Start () {

		foreach (Transform t in GetComponentsInChildren<Transform>()) {
		
			if (t.GetComponent<MeshCollider> ()) {
			
				t.GetComponent<MeshCollider> ().material = mat;
			
			}
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
