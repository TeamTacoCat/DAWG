using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {

	private bool playerActive;

	public bool PlayerActive{ 
	
		get {
		
			return playerActive;
		
		}
		set {

			if (value == true) {

				GetComponent<Image> ().color = Color.red;

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

			}

			playerReady = value;
		}
	}


	// Use this for initialization
	void OnEnable () {

		GetComponent<Image> ().color = Color.white;
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
			break;
		case 1:
			GetComponent<Image> ().color = Color.blue;
			break;
		case 2:
			GetComponent<Image> ().color = Color.green;
			break;
		case 3:
			GetComponent<Image> ().color = Color.yellow;
			break;
		default:
			break;
			

		}
	
	}
}
