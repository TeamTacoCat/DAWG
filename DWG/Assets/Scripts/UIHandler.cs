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

	// Use this for initialization
	void Start () {
		
		Match match = new Match ();
		match.PlayerType = new int[4];
		for(int i = 0;i<match.PlayerType.Length;i++){
			match.PlayerType[i] = -1;
		}

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
		
		}else{

			charSelection [pNumber].GetComponent<CharSelect> ().PlayerReady = true;

		}

	}

}