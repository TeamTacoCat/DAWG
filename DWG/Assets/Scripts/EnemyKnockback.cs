using UnityEngine;
using System.Collections;

public class EnemyKnockback : MonoBehaviour {

	public AudioClip[] chaseraud = new AudioClip[3]; //size = 3
	private GameObject parObj;
	public GameObject detector{ get; set; }
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
	
			parObj.transform.position = Vector3.MoveTowards (parObj.transform.position, new Vector3 (chaseTarget.transform.position.x, parObj.transform.position.y, chaseTarget.transform.position.z), speed);
			parObj.transform.LookAt (new Vector3 (chaseTarget.transform.position.x, transform.position.y, chaseTarget.transform.position.z));
			transform.parent.gameObject.GetComponentInChildren<Animator> ().SetBool ("Walk", true);
		
		} else if (parObj.transform.position != detector.transform.position) {
		
			parObj.transform.position = Vector3.MoveTowards (parObj.transform.position, detector.transform.position, speed);
			parObj.transform.LookAt (new Vector3 (detector.transform.position.x, transform.position.y, detector.transform.position.z));
			transform.parent.gameObject.GetComponentInChildren<Animator> ().SetBool ("Walk", true);
		
		} else {
		
			transform.parent.gameObject.GetComponentInChildren<Animator> ().SetBool ("Walk", false);
		
		}
	
	}

	void OnTriggerEnter(Collider coll){

		if (coll.tag == "Player") {
		
			SFX.sound.PlaySound (chaseraud [2]);
			Vector3 hitDir = (coll.transform.position-transform.position).normalized;
			coll.GetComponent<Rigidbody>().AddForce(hitDir*knockBack, ForceMode.Impulse);
		
		}

	}

}
