using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	[SerializeField]private float speedX;
	[SerializeField]private float speedY;
	[SerializeField]private float speedZ;
	[SerializeField]private float delay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (speedX, speedY, speedZ);
	
	}

	IEnumerator Destruct(){

		yield return new WaitForSeconds(delay);
		Destroy (this.gameObject);

	}
}
