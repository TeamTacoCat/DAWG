using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PUpDisplay : MonoBehaviour {

	private Player player;
	[SerializeField]private int pNumber;
	private Text text;
	[SerializeField]private Sprite[] powerSprites = new Sprite[10];
	private Image spriteImage;


	// Use this for initialization
	void Start () {

		player = GameObject.Find ("Player" + pNumber.ToString ()).GetComponent<Player>();
		if (GetComponent<Text> ()) {
			text = GetComponent<Text> ();
		}
		if (GetComponent<Image> ()) {
		
			spriteImage = GetComponent<Image> ();
		
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (text) {

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
			case 6:
				text.text = "Power Up: Knockback";
				break;
			case 7:
				text.text = "Power Up: Confusion";
				break;
			case 8:
				text.text = "Power Up: Oil Slick";
				break;

			}
		} else {
			
			spriteImage.sprite = powerSprites [player.powerUp+1];
			if (player.powerUp == -1) {
			
				spriteImage.color = new Color (1, 1, 1, 0);
			
			} else {

				spriteImage.color = new Color (1, 1, 1, 1);
			
			}


		}
	
	}
}
