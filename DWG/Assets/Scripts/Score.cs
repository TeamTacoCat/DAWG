using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	private Text score;
	public int [] PlayerScore{ get; set; }
	private int TeamNum;
	public Player player{ get; set; }


	// Use this for initialization
	void Start () {
	
		score = GetComponent<Text> ();
		PlayerScore = new int[4];
		TeamNum = player.GetTeamNumber ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
		PlayerScore = GameManager.GetPoints ();
		score.text = "Score: " + PlayerScore[TeamNum-1];

	}
}