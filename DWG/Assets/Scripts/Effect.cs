using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {


	//Where the world power ups are saved
	//[SerializeField]private Sprite[] worldIcon;
	private int spriteChoice;
	[SerializeField]private float timer;
	private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
		//GetComponent<SpriteRenderer> ().sprite = worldIcon [spriteChoice];
		mesh = transform.parent.GetComponentInChildren<MeshRenderer>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Player") {
			StartCoroutine ("pickedUp");
			spriteChoice = Random.Range (0, 9);
			coll.gameObject.GetComponent<Player> ().SetPowerUp (spriteChoice);
		}
	}

	IEnumerator pickedUp(){
		//GetComponent<SpriteRenderer> ().enabled = false;
		mesh.enabled = false;
		GetComponent<SphereCollider> ().enabled = false;

		yield return new WaitForSeconds (timer);

		//GetComponent<SpriteRenderer> ().enabled = true;
		mesh.enabled = true;
		GetComponent<SphereCollider> ().enabled = true;
	}
}
