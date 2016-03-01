using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {


	//Where the world power ups are saved
	[SerializeField]private Sprite[] worldIcon;
	[SerializeField]private int spriteChoice;
	[SerializeField]private float timer;

	// Use this for initialization
	void Start () {
		//GetComponent<SpriteRenderer> ().sprite = worldIcon [spriteChoice];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		StartCoroutine ("pickedUp");
		coll.gameObject.GetComponent<Player> ().SetPowerUp (spriteChoice);
	}

	IEnumerator pickedUp(){
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<SphereCollider> ().enabled = false;

		yield return new WaitForSeconds (timer);

		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<SphereCollider> ().enabled = true;
	}
}
