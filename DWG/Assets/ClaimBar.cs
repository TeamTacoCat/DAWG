using UnityEngine;
using System.Collections;

public class ClaimBar : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		this.GetComponent<RectTransform> ().localScale = new Vector3 (player.GetComponent<Player> ().sigilProg / 100, 1, 1);
	
	}
}
