using UnityEngine;
using System.Collections;

public class ShootEnemy : MonoBehaviour {

	[SerializeField]private GameObject detector;
	public bool detected=false;
	public GameObject chaseTarget;
	public float delay = 1.0f;
	public GameObject e_Bullet;
	public bool canShoot;

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

			print ("Shoot activated");
			GameObject instance;
			instance = (GameObject)Instantiate (e_Bullet, transform.position, transform.rotation);
			instance.transform.LookAt (chaseTarget.transform.position);
			instance.GetComponent<bulletImpact> ().target = chaseTarget;
			Invoke ("Shoot", delay);

		}
	}
}
