﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SigilSpawn : MonoBehaviour {

	public AudioClip loseaud;
	private GameObject[] grids = new GameObject[9];
	public GameObject minimapCanvas{ get; set; }
	private int sigilsSpawned;

	[SerializeField]private GameObject sigil;
	[SerializeField]private GameObject searchImage;
	[SerializeField]private GameObject claimedImage;
	[SerializeField]private GameObject winImage;
	private GameObject curSigil;
	private GameObject searchObj;
	private GameObject claimedObj;

	public GameObject menuHandler{ get; set; }

	private List<int> gridsDone = new List<int>();

	public bool starter = false;

	public TimeAttackClock timer{get; set;}

	private bool finished;

	// Use this for initialization
	void Start () {

		finished = false;

		Invoke ("TEMP", 3f);

		for (int i = 0; i < grids.Length; i++) {
		
			grids [i] = GameObject.Find ("GridLayout" + "/" + (i+1).ToString ());
		
		}
	
	}

	void TEMP(){
	
		SpawnSigil (5);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (starter) {
			if (gridsDone.Count <= 8) {

				if (GameManager.teams > 1) {

					CheckMajority ();
				
				} else {
				
					SinglePlayerResolution ();
				
				}

				if (curSigil == null) {
		
					ChooseGrid ();
		
				}
			} else if (GameManager.teams == 3) {
			
				TIE ();
			
			} else if (GameManager.teams == 1) {

				SinglePlayerResolution ();
			
			} else {
			
				CheckMajority ();
			
			}
		}
	
	}

	void CheckMajority(){

		int minToWin = Mathf.CeilToInt (9f / (float)GameManager.teams);
		int[] pointCopy = GameManager.GetPoints();
		bool over = true;

		for(int i = 0;i< pointCopy.Length;i++) {
		
			if (pointCopy[i] >= minToWin) {

				print ("team " + i + "above minimum amount");
			
				for (int j = 0; j < pointCopy.Length; j++) {
				
					if (j != i && pointCopy[j]+(9-(gridsDone.Count-1)) > pointCopy[i]) {
					
						print ("team still capable of winning, "+(9-(gridsDone.Count-1))+"grids left");
						over = false;
					
					}
				
				}

				if(over){

					starter = false;
					Finish(i+1);

				}
			
			}
		
		}

	}

	void SinglePlayerResolution(){

		if (timer) {
		
			if (timer.timeLeft <= 0) {
			
				Finish (7);
			
			} else if (gridsDone.Count >= 9) {
			
				Finish (6);
			
			}
		
		}


	}

	void TIE(){

		Finish (5);

	}


	void Finish(int team){

		if (finished == false) {

			finished = true;

			string teamName = " ";

			switch (team) {

			case 1:
				teamName = "Red";
				break;
			case 2:
				teamName = "Blue";
				break;
			case 3:
				teamName = "Green";
				break;
			case 4:
				teamName = "Yellow";
				break;
			default:
				break;

			}

			GameObject winObj = (GameObject)Instantiate (winImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
			winObj.transform.SetParent (minimapCanvas.transform, false);
			winObj.GetComponentInChildren<Text> ().text = teamName + " Team Wins!";
			//yield return new WaitForSeconds (3f);
			//GameManager.LoadScene ("MainMenu");

			if (team == 5) {
		
				winObj.GetComponentInChildren<Text> ().text = "Tie Game!";
		
			}
			if (team == 6) {
		
				winObj.GetComponentInChildren<Text> ().text = "You win!";
		
			}
			if (team == 7) {
		
				winObj.GetComponentInChildren<Text> ().text = "You lose!";
				SFX.sound.PlaySound (loseaud);
		
			}

			menuHandler.GetComponent<MenuHandler> ().MatchEnd ();

		}
	}

	void ChooseGrid(){

		int i;

			do{

			i = (int)Random.Range (1, 10);

			}while(gridsDone.Contains (i));

		SpawnSigil(i);

	}

	void SpawnSigil(int gridNumber){

		starter = true;

		Transform[] gridArray = grids [gridNumber-1].GetComponentsInChildren<Transform> ();

		print ("Grid Number" + gridNumber);

		curSigil = (GameObject)Instantiate (sigil, gridArray[(int)Random.Range (0, gridArray.Length)].position, Quaternion.Euler (0, 0, 0));
		curSigil.GetComponent<Sigil> ().grid = gridNumber;
		curSigil.GetComponent<Sigil> ().spawner = this.gameObject;
		print(curSigil.GetComponent<Sigil>().spawner.name);

		grids [gridNumber-1].GetComponent<GridDetection> ().activeSigil = curSigil;

		searchObj = (GameObject)Instantiate (searchImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		searchObj.transform.SetParent (minimapCanvas.transform, false);
		searchObj.GetComponent<RectTransform> ().localPosition = minimapCanvas.GetComponent<MinimapPos> ().GetGridPos (gridNumber);
		if (GameManager.teams == 1) {
		
			searchObj.GetComponent<RectTransform> ().localPosition = new Vector2 (searchObj.GetComponent<RectTransform> ().localPosition.x + 778, searchObj.GetComponent<RectTransform> ().localPosition.y + 346);
		
		}
		print ("Sigil spawned at:" + curSigil.transform.position);

		gridsDone.Add (gridNumber);

	}

	public void ClaimMap(int gridClaimed, int team){
	
		claimedObj = (GameObject)Instantiate (claimedImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		claimedObj.transform.SetParent (minimapCanvas.transform, false);
		claimedObj.GetComponent<RectTransform> ().localPosition = minimapCanvas.GetComponent<MinimapPos> ().GetGridPos(gridClaimed);
		if (GameManager.teams == 1) {

			claimedObj.GetComponent<RectTransform> ().localPosition = new Vector2 (claimedObj.GetComponent<RectTransform> ().localPosition.x + 778, claimedObj.GetComponent<RectTransform> ().localPosition.y + 346);

		}

		switch (team) {

		case 1:
			claimedObj.GetComponent<Image> ().color = new Color (1, 0, 0, .4f);
			break;
		case 2:
			claimedObj.GetComponent<Image> ().color = new Color (0, 0, 1, .4f);
			break;
		case 3:
			claimedObj.GetComponent<Image> ().color = new Color (0, 1, 0, .4f);
			break;
		case 4:
			claimedObj.GetComponent<Image> ().color = new Color (1, 1, 0, .4f);
			break;
		default:
			break;

		}

		Destroy (curSigil.gameObject);
		curSigil = null;
		Destroy (searchObj);
	
	}

}
