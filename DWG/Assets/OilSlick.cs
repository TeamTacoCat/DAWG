using UnityEngine;
using System.Collections;

public class OilSlick : MonoBehaviour {

	public int teamNumber { get; set; }

	[SerializeField] private AudioClip oilaud;

	// Use this for initialization
	void Start () {
		StartCoroutine ("OilTimer");
	}

	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.GetComponent<Player> ().teamNum != teamNumber) {

				SFX.sound.PlaySound (oilaud);

				coll.gameObject.GetComponent<PlayerController> ().slicked = true;
				coll.gameObject.GetComponent<PlayerController> ().StartCoroutine ("NoMovementTimer");
				StartCoroutine ("RandomMovement", coll.gameObject);
			}
		}
	}
	IEnumerator OilTimer(){
		yield return new WaitForSeconds (20.0f);
		Destroy (this.gameObject);
	}

	IEnumerator RandomMovement (GameObject player){
		int loop = 0;
		player.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-100, 101), 0, Random.Range (-100, 101));
		while (loop < 20) {
			player.transform.Rotate (0f, 5f, 0f);
			loop++;
			yield return null;
		}
	}

}
