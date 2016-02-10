using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SigilSpawn : MonoBehaviour {

	[SerializeField]private GameObject[] grids;
	[SerializeField]private GameObject minimapCanvas;
	private int sigilsSpawned;

	[SerializeField]private GameObject sigil;
	[SerializeField]private GameObject searchImage;
	[SerializeField]private GameObject claimedImage;
	private GameObject curSigil;
	private GameObject searchObj;
	private GameObject claimedObj;

	private List<int> gridsDone = new List<int>();

	public bool starter = false;

	// Use this for initialization
	void Start () {

		Invoke ("TEMP", 3f);
	
	}

	void TEMP(){
	
		SpawnSigil (5);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (starter) {
			if (gridsDone.Count <= 9) {
				if (curSigil == null) {
		
					ChooseGrid ();
		
				}
			}
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

		searchObj = (GameObject)Instantiate (searchImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		searchObj.transform.SetParent (minimapCanvas.transform, false);
		searchObj.GetComponent<RectTransform> ().localPosition = minimapCanvas.GetComponent<MinimapPos> ().GetGridPos(gridNumber);

		print ("Sigil spawned at:" + curSigil.transform.position);

		gridsDone.Add (gridNumber);

	}

	public void ClaimMap(int gridClaimed, int team){
	
		claimedObj = (GameObject)Instantiate (claimedImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		claimedObj.transform.SetParent (minimapCanvas.transform, false);
		claimedObj.GetComponent<RectTransform> ().localPosition = minimapCanvas.GetComponent<MinimapPos> ().GetGridPos(gridClaimed);

		switch (team) {

		case 1:
			claimedObj.GetComponent<UnityEngine.UI.Image> ().color = new Color (1, 0, 0, .2f);
			break;
		case 2:
			claimedObj.GetComponent<UnityEngine.UI.Image> ().color = new Color (0, 0, 1, .2f);
			break;
		case 3:
			claimedObj.GetComponent<UnityEngine.UI.Image> ().color = new Color (0, 1, 0, .2f);
			break;
		case 4:
			claimedObj.GetComponent<UnityEngine.UI.Image> ().color = new Color (1, 1, 0, .2f);
			break;
		default:
			break;

		}

		Destroy (curSigil.gameObject);
		curSigil = null;
		Destroy (searchObj);
	
	}

}
