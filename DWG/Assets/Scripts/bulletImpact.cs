using UnityEngine;
using System.Collections;

public class bulletImpact : MonoBehaviour {

	[SerializeField]private float knockBack;
	public GameObject target{ get; set; }
	[SerializeField]private float chaseDelay;
	[SerializeField]private float speed;
	[SerializeField]private GameObject oil;

	public int pUp{ get; set; }

	// Use this for initialization
	void Start () {

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

	void OnTriggerEnter(Collider coll){

		print ("Bullet hit"+coll.gameObject.name);

		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.GetComponent<Player> ().teamNum != pUp) {
				if (pUp == 5) {
				
					Vector3 hitDir = (coll.transform.position - transform.position).normalized;
					coll.GetComponent<Rigidbody> ().AddForce (hitDir * knockBack, ForceMode.Impulse);

				} else if (pUp == 6) {
			
					coll.gameObject.GetComponent<PlayerController> ().StartCoroutine ("Confusion");
			
				}
				else {

					GameObject instance = (GameObject)Instantiate (oil, this.transform.position, this.transform.rotation);
					instance.GetComponent<OilSlick> ().teamNumber = pUp;

				}
			}
		} else {
		
			GameObject instance = (GameObject)Instantiate (oil, this.transform.position, this.transform.rotation);
			instance.GetComponent<OilSlick> ().teamNumber = pUp;
		
		}
		Destroy (this.gameObject);
	
	}

	IEnumerator KillSelf(){

		yield return new WaitForSeconds(10f);
		Destroy (this.gameObject);

	}
}