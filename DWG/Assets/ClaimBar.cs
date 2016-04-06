using UnityEngine;
using System.Collections;

public class ClaimBar : MonoBehaviour {

	private Player player;
	[SerializeField]private int pNumber;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player" + pNumber.ToString ()).GetComponent<Player>();

	}
	
	// Update is called once per frame
	void Update () {

		this.GetComponent<RectTransform> ().localScale = new Vector3 (player.sigilProg / 100, 1, 1);
	
	}
}
