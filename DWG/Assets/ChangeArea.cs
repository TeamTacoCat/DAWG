using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeArea : MonoBehaviour {

	private List<Material> mat = new List<Material>();

	// Use this for initialization
	void Start () {

		foreach (Transform t in GetComponentsInChildren<Transform>()) {
	
			if (t.GetComponent<MeshRenderer> ()) {
			
				mat.Add(t.GetComponent<MeshRenderer> ().material);
			
			}
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeTo(Color c){
	
		foreach (Material m in mat) {
		
			m.color = c;
		
		}
	
	}
}
