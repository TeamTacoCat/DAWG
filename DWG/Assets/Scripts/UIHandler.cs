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
	[SerializeField]private GameObject[] charType;



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
			if (Input.GetButtonDown ("Jump1")) {

				SelectChar (0);

			}
			if (Input.GetButtonDown ("Jump2")) {

				SelectChar (1);

			}
			if (Input.GetButtonDown ("Jump3")) {

				SelectChar (2);

			}
			if (Input.GetButtonDown ("Jump4")) {

				SelectChar (3);

			}
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
			print ("Player 1 Type: " + match.PlayerType [0]);
			print ("Player 2 Type: " + match.PlayerType [1]);
			print ("Player 3 Type: " + match.PlayerType [2]);
			print ("Player 4 Type: " + match.PlayerType [3]);
					
		}else{

			charSelection [pNumber].GetComponent<CharSelect> ().PlayerReady = true;
			readyPlayers++;
			if (readyPlayers == match.NumPlayers && match.NumPlayers > 1) {
				startButton.SetActive(true);
			}
		}
	}

	public void starter(){

		//GameManager.StartSetup (match);
		//print(readyPlayers);

	}

	public void selectType(int arrowNum){
		
	}

}