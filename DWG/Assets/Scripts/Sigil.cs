using UnityEngine;
using System.Collections;

public class Sigil : MonoBehaviour {

	public int grid;
	public GameObject spawner;

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

	public void Claim(int teamNum){

		spawner.GetComponent<SigilSpawn> ().ClaimMap (grid, teamNum);

	}
}
