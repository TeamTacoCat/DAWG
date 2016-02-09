using UnityEngine;
using System.Collections;

public class Sigil : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll){
	
		if (coll.gameObject.tag == "Player") {

			coll.GetComponent<Player> ().sigil = this.gameObject;

			print (coll.name + "In Sigil Zone");

		}
	
	}

	void OnTriggerExit(Collider coll){
	
		if (coll.gameObject.tag == "Player") {
		
			coll.GetComponent<Player> ().sigil = null;
		
		}
	
	}
}
