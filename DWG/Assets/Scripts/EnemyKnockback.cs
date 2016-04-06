using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour {

	private GameObject parObj;
	[SerializeField]private GameObject detector;
	public bool detected=false;
	public GameObject chaseTarget;
	[SerializeField]private float speed;
	[SerializeField]private float knockBack;

	// Use this for initialization
	void Start () {

		parObj = transform.parent.gameObject;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (detected && chaseTarget != null) {
	
			parObj.transform.position = Vector3.MoveTowards (parObj.transform.position, new Vector3(chaseTarget.transform.position.x, parObj.transform.position.y, chaseTarget.transform.position.z), speed);
		
		} else if (parObj.transform.position != detector.transform.position) {
		
			parObj.transform.position = Vector3.MoveTowards (parObj.transform.position, detector.transform.position, speed);
		
		}
	
	}

	void OnTriggerEnter(Collider coll){

		if (coll.tag == "Player") {
		
			Vector3 hitDir = (coll.transform.position-transform.position).normalized;
			coll.GetComponent<Rigidbody>().AddForce(hitDir*knockBack, ForceMode.Impulse);
		
		}

	}

}
