using UnityEngine;
using System.Collections;

public class ShootEnemy : MonoBehaviour {

	public AudioClip[] shootaud = new AudioClip[3]; //size = 3
	public GameObject detector{ get; set; }
	public bool detected=false;
	public GameObject chaseTarget;
	public float delay = 1.0f;
	public GameObject e_Bullet;
	public bool canShoot;
	public GameObject head;

	// Use this for initialization
	void Start () {
		
		canShoot = false;
		Invoke ("Shoot", delay);

	}

	// Update is called once per frame
	void Update () {

		if (detected && chaseTarget != null && canShoot == false) {

			print ("Begin shooting");

			canShoot = true;
			Shoot ();
		
		} else if (!detected && canShoot == true) {

			canShoot = false;

		}

	}

	void Shoot () 
	{
		if (canShoot) {

			head.transform.LookAt (chaseTarget.transform.position);
			print ("Shoot activated");
			GameObject instance;
			instance = (GameObject)Instantiate (e_Bullet, transform.position, transform.rotation);
			SFX.sound.PlaySound (shootaud [2]);
			instance.transform.LookAt (chaseTarget.transform.position);
			instance.GetComponent<bulletImpact> ().pUp = 5;
			instance.GetComponent<bulletImpact> ().target = chaseTarget;
			Invoke ("Shoot", delay);

		}
	}
}
