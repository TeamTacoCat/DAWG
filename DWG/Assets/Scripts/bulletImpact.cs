using UnityEngine;
using System.Collections;

public class bulletImpact : MonoBehaviour {

	[SerializeField]private float knockBack;
	public GameObject target{ get; set; }
	[SerializeField]private float chaseDelay;
	[SerializeField]private float speed;

	// Use this for initialization
	void Start () {

		StartCoroutine ("HomingDelay");
	
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

		if (coll.gameObject.tag == "Player") {
			Vector3 hitDir = (coll.transform.position - transform.position).normalized;
			coll.GetComponent<Rigidbody> ().AddForce (hitDir * knockBack, ForceMode.Impulse);
		}
		Destroy (this.gameObject);
	
	}
}