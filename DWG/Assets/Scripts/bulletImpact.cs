using UnityEngine;
using System.Collections;

public class bulletImpact : MonoBehaviour {

	[SerializeField]private float knockBack;
	public GameObject target{ get; set; }
	[SerializeField]private float chaseDelay;
	public float speed{ get; set; }
	[SerializeField]private GameObject oil;

	public int pUp{ get; set; }

	[SerializeField] private AudioClip[] bulletaud;

	public GameObject player{ get; set; }

	// Use this for initialization
	void Start () {

		player = null;
		speed = .2f;
		StartCoroutine ("HomingDelay");
		StartCoroutine ("KillSelf");
	
	}
	
	// Update is called once per frame
	void Update () {

		if (target != null) {
		
			transform.LookAt (target.transform.position);
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed);
		
		} else {

			transform.Translate(transform.forward*speed, Space.World);

		}
	
	}

	IEnumerator HomingDelay(){
	
		yield return new WaitForSeconds (chaseDelay);
		target = null;
	
	}

	void OnTriggerStay(Collider coll){

		print ("Bullet hit"+coll.gameObject.name);

		if (coll.gameObject.tag == "Player") {
			//if (coll.gameObject.GetComponent<Player> ().teamNum != pUp) {
			if (player != coll.gameObject) {
				if (pUp == 5) {
				
					Vector3 hitDir = (coll.transform.position - transform.position).normalized;
					coll.GetComponent<Rigidbody> ().AddForce (hitDir * knockBack, ForceMode.Impulse);

					SFX.sound.PlaySound (bulletaud [0]);
					SFX.sound.PlaySound (bulletaud [1]);
					Destroy (this.gameObject);

				} else if (pUp == 6) {
			
					coll.gameObject.GetComponent<PlayerController> ().StartCoroutine ("Confusion");

					SFX.sound.PlaySound (bulletaud [2]);
					SFX.sound.PlaySound (bulletaud [1]);
					Destroy (this.gameObject);
			
				} else {

					GameObject instance = (GameObject)Instantiate (oil, this.transform.position, this.transform.rotation);
					instance.GetComponent<OilSlick> ().teamNumber = pUp;
					Destroy (this.gameObject);

				}
			}
			//}
		} else {

			print ("Non player has hit bullet");
		
			if (pUp != 5 || pUp != 6) {
				GameObject instance = (GameObject)Instantiate (oil, this.transform.position, this.transform.rotation);
				instance.GetComponent<OilSlick> ().teamNumber = pUp;
				Destroy (this.gameObject);
			}
		}

	
	}

	IEnumerator KillSelf(){

		yield return new WaitForSeconds(10f);
		Destroy (this.gameObject);

	}
}