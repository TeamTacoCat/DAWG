using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

	public GameObject enemy{ get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){

		if (coll.tag == "Player") {
		
			print ("Player detected");
			if (enemy.GetComponentInChildren<EnemyKnockback> ()) {
				SFX.sound.PlaySound (enemy.GetComponentInChildren<EnemyKnockback>().chaseraud[0]);
				enemy.GetComponentInChildren<EnemyKnockback> ().chaseTarget = coll.gameObject;
				enemy.GetComponentInChildren<EnemyKnockback> ().detected = true;
			} else if (enemy.GetComponentInChildren<ShootEnemy> ()) {
			
				print ("Shoot enemy found it");
				SFX.sound.PlaySound (enemy.GetComponentInChildren<ShootEnemy>().shootaud[0]);
				enemy.GetComponentInChildren<ShootEnemy> ().chaseTarget = coll.gameObject;
				enemy.GetComponentInChildren<ShootEnemy> ().detected = true;
			
			}
		}

	}

	void OnTriggerExit(Collider coll){

		if (coll.tag == "Player") {

			print ("Player has exited");
			if (enemy.GetComponentInChildren<EnemyKnockback> ()) {
				SFX.sound.PlaySound (enemy.GetComponentInChildren<EnemyKnockback>().chaseraud[1]);
				enemy.GetComponentInChildren<EnemyKnockback> ().detected = false;
				enemy.GetComponentInChildren<EnemyKnockback> ().chaseTarget = null;
			}else if (enemy.GetComponentInChildren<ShootEnemy> ()) {

				SFX.sound.PlaySound (enemy.GetComponentInChildren<ShootEnemy>().shootaud[1]);
				enemy.GetComponentInChildren<ShootEnemy> ().chaseTarget = null;
				enemy.GetComponentInChildren<ShootEnemy> ().detected = false;

			}
		}

	}
}
