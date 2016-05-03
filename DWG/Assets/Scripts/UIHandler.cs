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
	[SerializeField]private GameObject optButton;
	[SerializeField]private GameObject musicbutton;
	[SerializeField]private GameObject soundbutton;
	private Match match = new Match();
	private int readyPlayers;
	[SerializeField]private GameObject startButton;
	[SerializeField]private EventSystem events;
	private bool p1B;
	private bool p2B;
	private bool p3B;
	private bool p4B;

	private GameObject gManager;

	[SerializeField] private GameObject fullscreenonimg;
	[SerializeField] private GameObject smallscreenonimg;
	[SerializeField] private GameObject medscreenonimg;
	[SerializeField] private GameObject bigscreenonimg;
	[SerializeField] private RectTransform musicbarimg;
	[SerializeField] private RectTransform soundbarimg;
	[SerializeField] private GameObject togglebtn;

	private bool fullscreenActive;
	private bool smallscreenActive;
	private bool medscreenActive;
	private bool bigscreenActive;
	private int musicVol;
	private int soundVol;
	private int screenres = 0;
	private bool musicActive;
	private bool soundActive;
	private float maxmusicXbar = 122.6f;
	private float currentmusicXbar;
	private float maxsoundXbar = 122.6f;
	private float currentsoundXbar;

	private bool howtotoggle;
	[SerializeField] private GameObject howToScreen;
	[SerializeField] private GameObject controlsScreen;

	public AudioClip[] audclip;


	// Use this for initialization
	void Start () {
		
		match.PlayerType = new int[4];
		for(int i = 0;i<match.PlayerType.Length;i++){
			match.PlayerType[i] = -1;
		}
		match.NumPlayers = 0;
		readyPlayers = 0;
		startButton.SetActive (false);

		gManager = GameObject.Find ("GameManager");

		fullscreenActive = true;
		Screen.fullScreen = true;
		smallscreenActive = false;
		medscreenActive = false;
		bigscreenActive = false;
		musicVol = 1;
		soundVol = 1;
		musicActive = false;
		soundActive = false;

		currentmusicXbar = GameManager.bgmVolume * maxmusicXbar;

		howtotoggle = true;
	}

	// Update is called once per frame
	void Update () {

		if (fullscreenActive) {
			fullscreenonimg.SetActive (true);
		} else {
			fullscreenonimg.SetActive (false);
		}

		if (smallscreenActive) {
			smallscreenonimg.SetActive (true);
		} else {
			smallscreenonimg.SetActive (false);
		}

		if (medscreenActive) {
			medscreenonimg.SetActive (true);
		} else {
			medscreenonimg.SetActive (false);
		}

		if (bigscreenActive) {
			bigscreenonimg.SetActive (true);
		} else {
			bigscreenonimg.SetActive (false);
		}



		switch(activeMenu.name){

		case "MainMenu":
			//probably doubling up the sound... not sure how to tighten the restrictions on this!
			if (Input.GetAxis ("Vertical1") != 0 || Input.GetAxis ("Vertical2") != 0 || Input.GetAxis ("Vertical3") != 0 || Input.GetAxis ("Vertical4") != 0) {
				SFX.sound.PlaySound (audclip [2]);
			}

			break;

		case "MultMenu":
			
			print ("Multi Menu active");
			if (Input.GetButtonDown ("Jump1")) {
				print ("player 1 active");
					SelectChar (0);
					SFX.sound.PlaySound(audclip[0]);

			}
			if (Input.GetButtonDown ("Jump2")) {
				print ("Player 2 active");
				SelectChar (1);
				SFX.sound.PlaySound(audclip[0]);

			}
			if (Input.GetButtonDown ("Jump3")) {
				
				SelectChar (2);
				SFX.sound.PlaySound(audclip[0]);

			}
			if (Input.GetButtonDown ("Jump4")) {

				SelectChar (3);
				SFX.sound.PlaySound(audclip[0]);

			}
			/*if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {
				print ("Go back");
				SwitchMenu ("MainMenu");
				SFX.sound.PlaySound(audclip[1]);
			}*/
			if (Input.GetButtonDown ("Dash1")){
				if (charSelection [0].GetComponent<CharSelect> ().PlayerReady) {
					charSelection [0].GetComponent<CharSelect> ().PlayerReady = false;
					readyPlayers--;
					startButton.SetActive (false);

				} else if (charSelection [0].GetComponent<CharSelect> ().PlayerActive) {
					charSelection [0].GetComponent<CharSelect> ().PlayerActive = false;
					match.NumPlayers--;
					match.PlayerType [0] = 0;
											
						if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
							if (teamCheck ()) {
								startButton.SetActive (true);
							}
						}
				} else {
					SwitchMenu ("MainMenu");
					SFX.sound.PlaySound (audclip [1]);
					charSelection [0].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [0].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerReady = false;
				}
			}

			if (Input.GetButtonDown ("Dash2")){
				if (charSelection [1].GetComponent<CharSelect> ().PlayerReady) {
					charSelection [1].GetComponent<CharSelect> ().PlayerReady = false;
					readyPlayers--;
					startButton.SetActive (false);

				} else if (charSelection [1].GetComponent<CharSelect> ().PlayerActive) {
					charSelection [1].GetComponent<CharSelect> ().PlayerActive = false;
					match.NumPlayers--;
					match.PlayerType [1] = 0;

					if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
						if (teamCheck ()) {
							startButton.SetActive (true);
						}
					}
				} else {
					SwitchMenu ("MainMenu");
					SFX.sound.PlaySound (audclip [1]);
					charSelection [0].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [0].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerReady = false;
				}
			}

			if (Input.GetButtonDown ("Dash3")){
				if (charSelection [2].GetComponent<CharSelect> ().PlayerReady) {
					charSelection [2].GetComponent<CharSelect> ().PlayerReady = false;
					readyPlayers--;
					startButton.SetActive (false);

				} else if (charSelection [2].GetComponent<CharSelect> ().PlayerActive) {
					charSelection [2].GetComponent<CharSelect> ().PlayerActive = false;
					match.NumPlayers--;
					match.PlayerType [2] = 0;

					if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
						if (teamCheck ()) {
							startButton.SetActive (true);
						}
					}
				} else {
					SwitchMenu ("MainMenu");
					SFX.sound.PlaySound (audclip [1]);
					charSelection [0].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [0].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerReady = false;
				}
			}

			if (Input.GetButtonDown ("Dash4")){
				if (charSelection [3].GetComponent<CharSelect> ().PlayerReady) {
					charSelection [3].GetComponent<CharSelect> ().PlayerReady = false;
					readyPlayers--;
					startButton.SetActive (false);

				} else if (charSelection [3].GetComponent<CharSelect> ().PlayerActive) {
					charSelection [3].GetComponent<CharSelect> ().PlayerActive = false;
					match.NumPlayers--;
					match.PlayerType [3] = 0;

					if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
						if (teamCheck ()) {
							startButton.SetActive (true);
						}
					}
				} else {
					SwitchMenu ("MainMenu");
					SFX.sound.PlaySound (audclip [1]);
					charSelection [0].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerActive = false;
					charSelection [0].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [1].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [2].GetComponent<CharSelect> ().PlayerReady = false;
					charSelection [3].GetComponent<CharSelect> ().PlayerReady = false;
				}
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

					if (Input.GetAxis ("Horizontal1") != 0) {
						SFX.sound.PlaySound (audclip [2]);
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

					if (Input.GetAxis ("Horizontal2") != 0) {
						SFX.sound.PlaySound (audclip [2]);
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

				if (Input.GetAxis ("Horizontal3") != 0) {
					SFX.sound.PlaySound (audclip [2]);
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

				if (Input.GetAxis ("Horizontal4") != 0) {
					SFX.sound.PlaySound (audclip [2]);
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
			if (Input.GetAxis ("Horizontal3") == 0 /*<= 0.2f && Input.GetAxis ("Horizontal3") >= -0.2f*/) {

				p3B = true;

			} else {

				p3B = false;

			}
			if (Input.GetAxis ("Horizontal4") == 0/*Input.GetAxis ("Horizontal4") <= 0.2f && Input.GetAxis ("Horizontal4") >= -0.2f*/) {

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
				SFX.sound.PlaySound (audclip [1]);
							
			}

			if (Input.GetButtonDown ("Jump1") || Input.GetButtonDown ("Jump2") || Input.GetButtonDown ("Jump3") || Input.GetButtonDown ("Jump4")) {

				SFX.sound.PlaySound (audclip [0]);

			}

			break;
		case "OptionsMenu":

			if (Input.GetButtonDown ("Jump1") || Input.GetButtonDown ("Jump2") || Input.GetButtonDown ("Jump3") || Input.GetButtonDown ("Jump4")) {

				SFX.sound.PlaySound (audclip [0]);

			}

			if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {

				SwitchMenu ("MainMenu");
				SFX.sound.PlaySound (audclip [1]);

			}

			//probably doubling up the sound... not sure how to tighten the restrictions on this!
			if (Input.GetAxis ("Vertical1") != 0 || Input.GetAxis ("Vertical2") != 0 || Input.GetAxis ("Vertical3") != 0 || Input.GetAxis ("Vertical4") != 0) {
				SFX.sound.PlaySound (audclip [2]);
			}

			if (events.currentSelectedGameObject == musicbutton) {
				if (Input.GetAxis ("Horizontal1") > 0 || Input.GetAxis ("Horizontal2") > 0 || Input.GetAxis ("Horizontal3") > 0 || Input.GetAxis ("Horizontal4") > 0) {
					
					if (currentmusicXbar <= maxmusicXbar) {
						currentmusicXbar += 1;
						musicbarimg.sizeDelta = new Vector2(currentmusicXbar, 17.7f);
						GameManager.bgmVolume = (currentmusicXbar / maxmusicXbar);
						print ("Current Music Volume: " + GameManager.bgmVolume);
						SFX.sound.PlaySound (audclip [2]);
					}

				} else if (Input.GetAxis ("Horizontal1") < 0 || Input.GetAxis ("Horizontal2") < 0 || Input.GetAxis ("Horizontal3") < 0 || Input.GetAxis ("Horizontal4") < 0) {
					
					if (currentmusicXbar >= 0) {
						currentmusicXbar -= 1;
						musicbarimg.sizeDelta = new Vector2(currentmusicXbar, 17.7f);
						GameManager.bgmVolume = (currentmusicXbar / maxmusicXbar); 
						print ("Current Music Volume: " + GameManager.bgmVolume);
						SFX.sound.PlaySound (audclip [2]);
					}
				}
			} else if (events.currentSelectedGameObject == soundbutton) {
				if (Input.GetAxis ("Horizontal1") > 0 || Input.GetAxis ("Horizontal2") > 0 || Input.GetAxis ("Horizontal3") > 0 || Input.GetAxis ("Horizontal4") > 0) {

					if (currentsoundXbar <= maxsoundXbar) {
						currentsoundXbar += 1;
						soundbarimg.sizeDelta = new Vector2(currentsoundXbar, 17.7f);
						GameManager.sfxVolume = (currentsoundXbar / maxsoundXbar);
						print ("Current Sound Volume: " + GameManager.sfxVolume);
						SFX.sound.PlaySound (audclip [2]);
					}

				} else if (Input.GetAxis ("Horizontal1") < 0 || Input.GetAxis ("Horizontal2") < 0 || Input.GetAxis ("Horizontal3") < 0 || Input.GetAxis ("Horizontal4") < 0) {

					if (currentsoundXbar >= 0) {
						currentsoundXbar -= 1;
						soundbarimg.sizeDelta = new Vector2(currentsoundXbar, 17.7f);
						GameManager.sfxVolume = (currentsoundXbar / maxsoundXbar); 
						print ("Current Sound Volume: " + GameManager.sfxVolume);
						SFX.sound.PlaySound (audclip [2]);
					}
				}
			}
			break;

		case "HowtoMenu":
			if (Input.GetButtonDown ("Dash1") || Input.GetButtonDown ("Dash2") || Input.GetButtonDown ("Dash3") || Input.GetButtonDown ("Dash4")) {

				SwitchMenu ("OptionsMenu");
				SFX.sound.PlaySound (audclip [1]);

			}

			if (Input.GetButtonDown ("Jump1") || Input.GetButtonDown ("Jump2") || Input.GetButtonDown ("Jump3") || Input.GetButtonDown ("Jump4")) {
				SFX.sound.PlaySound (audclip [0]);
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
			match.MatchType = "LevelLayout";
			break;
		case "SingMenu":
			/*
			menus [2].SetActive (true);
			activeMenu = menus [2];
			*/
			match.MatchType = "SinglePlayerLevel";
			match.NumPlayers = 1;
			match.PlayerType [0] = 0;
			starter ();
			break;
		case "OptionsMenu":
			menus [3].SetActive (true);
			activeMenu = menus [3];
			events.SetSelectedGameObject (optButton, null);
			
			break;

		case "HowtoMenu":
			menus [4].SetActive (true);
			activeMenu = menus [4];
			events.SetSelectedGameObject (togglebtn, null);
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
			if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
				if (teamCheck ()) {
					startButton.SetActive (true);
				}
			} else {
				startButton.SetActive (false);
			}
								
		} else {

			charSelection [pNumber].GetComponent<CharSelect> ().PlayerReady = true;
			readyPlayers++;
			if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
				if (teamCheck ()) {
					startButton.SetActive (true);
				}
			}
		}
	}

	public void starter(){

		gManager.GetComponent<GameManager>().StartSetup (match);
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

	public void options(string optbtn){



		switch (optbtn) {

		case "Fullscreenbtn":

			if (fullscreenActive) {
				fullscreenActive = false;
				Screen.SetResolution (1280, 720, false);
			} else {
				fullscreenActive = true;
				switch (screenres) {

				case 0:
					bigscreenActive = false;
					smallscreenActive = true;
					Screen.SetResolution (1024, 576, true);
					print ("Small Screen");
					break;
				case 1:
					smallscreenActive = false;
					medscreenActive = true;
					Screen.SetResolution (1280, 720, true);
					print ("Medium Screen");
					break;
				case 2:
					medscreenActive = false;
					bigscreenActive = true;
					Screen.SetResolution (1920, 1080, true);
					print ("Big screen");
					break;

				}
				//Screen.SetResolution (Screen.width, Screen.height, true);
				//smallscreenActive = false;
				//medscreenActive = false;
				//bigscreenActive = false;
			}

			break;
		
		
		case "Screensizebtn":

			if (fullscreenActive == false) {
				if (screenres == 0) {
					screenres++;
					bigscreenActive = false;
					smallscreenActive = true;
					Screen.SetResolution (1024, 576, false);
					print ("Small Screen");
				} else if (screenres == 1) {
					screenres++;
					smallscreenActive = false;
					medscreenActive = true;
					Screen.SetResolution (1280, 720, false);
					print ("Medium Screen");
				} else if (screenres == 2) {
					screenres = 0;
					medscreenActive = false;
					bigscreenActive = true;
					Screen.SetResolution (1920, 1080, false);
					print ("Big screen");
				}
			} else {
			
				if (screenres == 0) {
					screenres++;
					bigscreenActive = false;
					smallscreenActive = true;
					Screen.SetResolution (1024, 576, true);
					print ("Small Screen");
				} else if (screenres == 1) {
					screenres++;
					smallscreenActive = false;
					medscreenActive = true;
					Screen.SetResolution (1280, 720, true);
					print ("Medium Screen");
				} else if (screenres == 2) {
					screenres = 0;
					medscreenActive = false;
					bigscreenActive = true;
					Screen.SetResolution (1920, 1080, true);
					print ("Big screen");
				}


			}

			break;
		
		case "Howtobtn":
			print ("Howto pressed");

			break;
		
		
		default:
			break;
		}
	
	}

	public void toggleHowTo(){
		
		if (howtotoggle) {
			howtotoggle = false;
			controlsScreen.SetActive (true);
			howToScreen.SetActive (false);
		} else {
			howtotoggle = true;
			controlsScreen.SetActive (false);
			howToScreen.SetActive (true);
		}
			
	}

}