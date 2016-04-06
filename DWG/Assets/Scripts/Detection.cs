using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

	[SerializeField]private GameObject enemy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){

		if (coll.tag == "Player") {
		
			enemy.GetComponentInChildren<EnemyKnockback> ().chaseTarget = coll.gameObject;
			enemy.GetComponentInChildren<EnemyKnockback> ().detected = true;
		
		}

	}

	void OnTriggerExit(Collider coll){

		if (coll.tag == "Player") {

			enemy.GetComponentInChildren<EnemyKnockback> ().detected = false;
			enemy.GetComponentInChildren<EnemyKnockback> ().chaseTarget = null;

		}

	}
}
