using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {

	private bool playerActive;

	[SerializeField] private GameObject charselect;
	[SerializeField] private GameObject charchosen;
	[SerializeField] private Sprite redselect;
	[SerializeField] private Sprite redchosen;
	[SerializeField] private Sprite blueselect;
	[SerializeField] private Sprite bluechosen;
	[SerializeField] private Sprite greenselect;
	[SerializeField] private Sprite greenchosen;
	[SerializeField] private Sprite yellowselect;
	[SerializeField] private Sprite yellowchosen;

	public bool PlayerActive{ 
	
		get {
		
			return playerActive;
		
		}
		set {

			if (value == true) {

				GetComponent<Image> ().color = Color.red;
				charselect.SetActive (true);
			}

			playerActive = value;
		}
	}

	private bool playerReady;
	public bool PlayerReady { 
		get {
		
			return playerReady;
		
		}
		set {

			if (value == true) {
			
				GetComponent<Image> ().color = Color.magenta;
				charselect.SetActive (false);
				charchosen.SetActive (true);
				if (charselect.GetComponent<Image> ().sprite == redselect) {
					charchosen.GetComponent<Image> ().sprite = redchosen;
				} else if (charselect.GetComponent<Image> ().sprite == blueselect) {
					charchosen.GetComponent<Image> ().sprite = bluechosen;
				} else if (charselect.GetComponent<Image> ().sprite == greenselect) {
					charchosen.GetComponent<Image> ().sprite = greenchosen;
				} else if (charselect.GetComponent<Image> ().sprite == yellowselect) {
					charchosen.GetComponent<Image> ().sprite = yellowchosen;
				}


			}

			playerReady = value;
		}
	}


	// Use this for initialization
	void OnEnable () {

		GetComponent<Image> ().color = Color.white;
		charselect.SetActive (false);
		charchosen.SetActive (false);
		charselect.GetComponent<Image> ().sprite = redselect;
		charchosen.GetComponent<Image> ().sprite = redchosen;
		PlayerActive = false;
		PlayerReady = false;
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void imageSwitch(int imageNum){
	
		switch (imageNum) {

		case 0:
			GetComponent<Image> ().color = Color.red;
			charselect.GetComponent<Image> ().sprite = redselect;
			break;
		case 1:
			GetComponent<Image> ().color = Color.blue;
			charselect.GetComponent<Image> ().sprite = blueselect;
			break;
		case 2:
			GetComponent<Image> ().color = Color.green;
			charselect.GetComponent<Image> ().sprite = greenselect;
			break;
		case 3:
			GetComponent<Image> ().color = Color.yellow;
			charselect.GetComponent<Image> ().sprite = yellowselect;
			break;
		default:
			break;
			

		}
	
	}
}
