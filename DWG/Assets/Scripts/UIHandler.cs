using UnityEngine;
using System.Collections;
public class UIHandler : MonoBehaviour {


	public class Match{
		
		public string MatchType{ get; set; }
		public int NumPlayers{ get; set; }
		public int[] PlayerType{ get; set; }

	}

	[SerializeField]private GameObject[] menus;
	[SerializeField]private GameObject[] charSelection;
	private GameObject activeMenu;
	private Match match = new Match();
	private int readyPlayers;
	[SerializeField]private GameObject startButton;

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
		print(match.NumPlayers);

	}

}