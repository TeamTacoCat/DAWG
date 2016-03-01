using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour {


	public class Match{
		
		public string MatchType{ get; set; }
		public int NumPlayers{ get; set; }
		public int[] PlayerType{ get; set; } //0 = FemRed, 1= FemBlue, 2= FemYellow, 3= FemGreen

	}

	[SerializeField]private GameObject[] menus;
	[SerializeField]private GameObject button;
	[SerializeField]private GameObject[] charSelection;
	[SerializeField]private GameObject activeMenu;
	private Match match = new Match();
	private int readyPlayers;
	[SerializeField]private GameObject startButton;
	[SerializeField]private EventSystem events;
	private bool p1B;
	private bool p2B;
	private bool p3B;
	private bool p4B;




	// Use this for initialization
	void Start () {
		
		match.PlayerType = new int[4];
		for(int i = 0;i<match.PlayerType.Length;i++){
			match.PlayerType[i] = -1;
		}
		match.NumPlayers = 0;
		readyPlayers = 0;
		startButton.SetActive (false);


	}

	// Update is called once per frame
	void Update () {

		switch(activeMenu.name){

		case "MultMenu":
			print ("Multi Menu active");
			if (Input.GetButtonDown ("Jump1")) {
				print ("player 1 active");
				SelectChar (0);

			}
			if (Input.GetButtonDown ("Jump2")) {
				print ("Player 2 active");
				SelectChar (1);

			}
			if (Input.GetButtonDown ("Jump3")) {
				
				SelectChar (2);

			}
			if (Input.GetButtonDown ("Jump4")) {

				SelectChar (3);

			}
			if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {
				print ("Go back");
				SwitchMenu ("MainMenu");
			}

			////////////////PLAYER ONE////////////////////////////////////
			///
			if (charSelection [0].GetComponent<CharSelect> ().PlayerActive && !charSelection [0].GetComponent<CharSelect> ().PlayerReady) { 
				print ("Player 1 active but not ready");
				if (p1B) {
					if (Input.GetAxis ("Horizontal1") > 0) {
						print ("Player 1 horizontal1 above 0");
						if (match.PlayerType [0] + 1 == 4) {
					
							match.PlayerType [0] = 0;
					
						} else {
							match.PlayerType [0]++;
						}

						charSelection [0].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [0]);

					} else if (Input.GetAxis ("Horizontal1") < 0) {

						if (match.PlayerType [0] - 1 == -1) {

							match.PlayerType [0] = 3;
						} else {
							match.PlayerType [0]--;
						}

						charSelection [0].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [0]);
					}
				}
			}

			////////////////PLAYER TWO////////////////////////////////////
			///
			if (charSelection [1].GetComponent<CharSelect> ().PlayerActive && !charSelection [1].GetComponent<CharSelect> ().PlayerReady) {
				if (p2B) {
					if (Input.GetAxis ("Horizontal2") > 0) {
						if (match.PlayerType [1] + 1 == 4) {

							match.PlayerType [1] = 0;

						} else {
							match.PlayerType [1]++;
						}

						charSelection [1].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [1]);

					} else if (Input.GetAxis ("Horizontal2") < 0) {

						if (match.PlayerType [1] - 1 == -1) {

							match.PlayerType [1] = 3;
						} else {
							match.PlayerType [1]--;
						}

						charSelection [1].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [1]);
					}
				}
			}

			////////////////PLAYER THREE////////////////////////////////////
			///
			if (charSelection [2].GetComponent<CharSelect> ().PlayerActive && !charSelection [2].GetComponent<CharSelect> ().PlayerReady) {
				if (p3B) {
					if (Input.GetAxis ("Horizontal3") > 0) {
						if (match.PlayerType [2] + 1 == 4) {

							match.PlayerType [2] = 0;

						} else {
							match.PlayerType [2]++;
						}

						charSelection [2].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [0]);

					} else if (Input.GetAxis ("Horizontal3") < 0) {

						if (match.PlayerType [2] - 1 == -1) {

							match.PlayerType [2] = 3;
						} else {
							match.PlayerType [2]--;
						}

						charSelection [2].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [2]);
					}
				}
			}

			////////////////PLAYER FOUR////////////////////////////////////
			///
			if (charSelection [3].GetComponent<CharSelect> ().PlayerActive && !charSelection [3].GetComponent<CharSelect> ().PlayerReady) {
				if (p4B) {
					if (Input.GetAxis ("Horizontal4") > 0) {
						if (match.PlayerType [3] + 1 == 4) {

							match.PlayerType [3] = 0;

						} else {
							match.PlayerType [3]++;
						}

						charSelection [3].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [3]);

					} else if (Input.GetAxis ("Horizontal4") < 0) {

						if (match.PlayerType [3] - 1 == -1) {

							match.PlayerType [3] = 3;
						} else {
							match.PlayerType [3]--;
						}

						charSelection [3].GetComponent<CharSelect> ().imageSwitch (match.PlayerType [3]);
					}
				}
			}
			if (Input.GetAxis ("Horizontal1") == 0 /*<= 0.2f && Input.GetAxis ("Horizontal1") >= -0.2f*/) {

				p1B = true;

			} else {

				p1B = false;

			}
			print ("p1B: " + p1B + "\nHorizontal1: " + Input.GetAxis ("Horizontal1"));
			if (Input.GetAxis ("Horizontal2") == 0 /* <= 0.2f && Input.GetAxis ("Horizontal2") >= -0.2f*/) {

				p2B = true;

			} else {

				p2B = false;

			}
			if (Input.GetAxis ("Horizontal3") == 0 /*<= 0.2f && Input.GetAxis ("Horizontal3") >= -0.2f) {

				p3B = true;

			} else {

				p3B = false;

			}
			if (Input.GetAxis ("Horizontal4") <= 0.2f && Input.GetAxis ("Horizontal4") >= -0.2f*/) {

				p4B = true;

			} else {

				p4B = false;

			}
			if (startButton.activeSelf) {

				if (Input.GetButtonDown ("Start")) {

					starter ();

				}


			}
			break;
		case "SingMenu":
			if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {
			
				SwitchMenu ("MainMenu");
			
			}
			break;
		case "OptionsMenu":
			if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {

				SwitchMenu ("MainMenu");

			}
			break;
		default:
			break;
		
		}
	}

	public void TestFunction(int testNumber){
		if (testNumber == 10) {
			print ("YAY!");
		} else {

			print("FUCK OFF");

		}
	}

	public void SwitchMenu(string menu){
	
		if (activeMenu) {
		
			activeMenu.SetActive (false);
		
		}

		switch (menu) {

		case "MainMenu":
			menus [0].SetActive (true);
			activeMenu = menus [0];
			match.NumPlayers = 0;
			readyPlayers = 0;
			startButton.SetActive (false);
			events.SetSelectedGameObject (button, null);
			break;
		case "MultMenu":
			menus [1].SetActive (true);
			activeMenu = menus [1];
			
			break;
		case "SingMenu":
			menus [2].SetActive (true);
			activeMenu = menus [2];
			
			break;
		case "OptionsMenu":
			menus [3].SetActive (true);
			activeMenu = menus [3];
			
			break;
		default:
			break;
		}
	}

	public void SelectChar(int pNumber){

		if (!charSelection [pNumber].GetComponent<CharSelect> ().PlayerActive) {
		
			charSelection [pNumber].GetComponent<CharSelect> ().PlayerActive = true;
			match.NumPlayers++;
			match.PlayerType [pNumber] = 0;
								
		}else{

			charSelection [pNumber].GetComponent<CharSelect> ().PlayerReady = true;
			readyPlayers++;
			if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
				if (teamCheck()) {
					startButton.SetActive(true);
				}
			}
		}
	}

	public void starter(){

		//GameManager.StartSetup (match);
		print("Sending the match off!");

	}

	public bool teamCheck(){
	
		int teams = 0;
		int[] teamRoster = new int[4];

		for (int i = 0; i < teamRoster.Length; i++) {

			teamRoster [i] = 0;

		}

		for (int i = 0; i < match.PlayerType.Length; i++) {
			if (match.PlayerType[i] != -1) {
				teamRoster [match.PlayerType [i]]++;
			}

		}

			
		for (int i = 0; i < teamRoster.Length; i++) {

			if (teamRoster [i] > 0) {

				teams++;

			}

		}

		if (teams > 1) {
			return true;
		} else {
			return false;
		}
	
	}

}