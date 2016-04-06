using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searcher : MonoBehaviour {

	public GameObject player;
	public GameObject sigil;
	[SerializeField]private float maxTime;

	// Use this for initialization
	void Start () {
	
		player.GetComponent<Player> ().searcher = this.gameObject;
		StartCoroutine ("Blink");

	}
	
	// Update is called once per frame
	void Update () {


	}

	IEnumerator Blink(){

		bool loop = true;

		while (loop) {
			while (sigil != null) {
		
				print ("In Searcher, sigil != null");
				GetComponent<Image> ().color = new Color (0, 0, 0);
				yield return new WaitForSeconds (DistanceTime());
				if(sigil && player){
				GetComponent<Image> ().color = ColorDistance();
				}
				yield return new WaitForSeconds (.1f);
		
			}

			GetComponent<Image> ().color = new Color (0, 0, 0);
			yield return new WaitForEndOfFrame ();

		}

	}

	float DistanceTime(){
	
		float finalTime = (Vector3.Distance (player.transform.position, sigil.transform.position) * maxTime) / 509;
		return finalTime;
	
	}

	Color ColorDistance(){

		Color c;

		if (DistanceTime () > .7) {
		
			c = Color.white;

		} else if (DistanceTime () > .3f) {

			c = Color.yellow;

		}else {
		
			c = Color.red;

		}

		return c;

	}

}
