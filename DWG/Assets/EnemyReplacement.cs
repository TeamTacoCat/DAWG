using UnityEngine;
using System.Collections;

public class EnemyReplacement : MonoBehaviour {

	[SerializeField]private int which;
	[SerializeField]private GameObject chase;
	[SerializeField]private GameObject shoot;
	[SerializeField]private GameObject detect;

	// Use this for initialization
	void Start () {
		GameObject dInstance = (GameObject)Instantiate (detect, transform.position, transform.rotation);
		GameObject instance = null;

		switch (which) {

		case 1:
			instance = (GameObject)Instantiate (chase, transform.position, transform.rotation);
			instance.GetComponentInChildren<EnemyKnockback> ().detector = dInstance;
			break;
		case 2:
			instance = (GameObject)Instantiate (shoot, transform.position, transform.rotation);
			instance.GetComponentInChildren<ShootEnemy> ().detector = dInstance;
			break;
		default:
			break;
		}

		dInstance.GetComponent<Detection> ().enemy = instance;
		Destroy (this.gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
