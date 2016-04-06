using UnityEngine;
using System.Collections;

public class GridDetection : MonoBehaviour {

	public GameObject activeSigil;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
	
		if (activeSigil != null) {
		
			if (coll.tag == "Player") {
			
				print ("Player has entered grid, activeSigil != null");
				coll.GetComponent<Player> ().searcher.GetComponent<Searcher> ().sigil = activeSigil;
			
			}
		
		}
	
	}

	void OnTriggerExit(Collider coll){
	
		if (activeSigil != null) {

			if (coll.tag == "Player") {

				coll.GetComponent<Player> ().searcher.GetComponent<Searcher> ().sigil = null;

			}

		}
	
	}


}
