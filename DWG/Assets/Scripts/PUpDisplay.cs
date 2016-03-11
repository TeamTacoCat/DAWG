using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PUpDisplay : MonoBehaviour {

	private Player player;
	[SerializeField]private int pNumber;
	private Text text;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player" + pNumber.ToString ()).GetComponent<Player>();
		text = GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

		switch (player.powerUp) {

		case -1:
			text.text = "Power Up: None";
			break;
		case 0:
			text.text = "Power Up: Slipstream";
				break;
		case 1:
			text.text = "Power Up: Fuel Restore";
				break;
		case 2:
			text.text = "Power Up: Spell Slinger";
				break;
		case 3:
			text.text = "Power Up: Discombobulate";
				break;
		case 4:
			text.text = "Power Up: Grounded";
				break;
		case 5:
			text.text = "Power Up: Speed Cutter";
				break;

		}
	
	}
}
