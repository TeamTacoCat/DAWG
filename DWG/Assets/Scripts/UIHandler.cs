using UnityEngine;
using System.Collections;

public class UIHandler : MonoBehaviour {

	public class Match{
		public string MatchType{ get; set; }
		public int NumPlayers{ get; set; }
		public int[] PlayerType{ get; set; }
	}
		 
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
}
