using UnityEngine;
using System.Collections;

public class FBar : MonoBehaviour {

	private PlayerController player;
	[SerializeField]private int pNumber;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player" + pNumber.ToString ()).GetComponent<PlayerController>();

	}

	// Update is called once per frame
	void Update () {

		this.GetComponent<RectTransform> ().localScale = new Vector3 (player.currentFuel / player.maxFuel, 1, 1);

	}
}
