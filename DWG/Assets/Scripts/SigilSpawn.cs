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


	// Use this for initialization
	void Start () {

		SpawnSigil (5);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (gridsDone.Count <= 9) {
			if (curSigil == null) {
		
				ChooseGrid ();
		
			}
		}
	
	}

	void ChooseGrid(){

		int i;

			do{

			i = Random.Range (1, 10);

			}while(gridsDone.Contains (i));

		SpawnSigil(i);

	}

	void SpawnSigil(int gridNumber){

		Transform[] gridArray = grids [gridNumber].GetComponentsInChildren<Transform> ();

		curSigil = (GameObject)Instantiate (sigil, gridArray[Random.Range (0, gridArray.Length)].position, Quaternion.Euler (0, 0, 0));
		curSigil.GetComponent<Sigil> ().grid = gridNumber;

		searchObj = (GameObject)Instantiate (searchImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		searchObj.transform.SetParent (minimapCanvas.transform, false);
		//searchObj.GetComponent<RectTransform>().position

		print ("Sigil spawned at:" + curSigil.transform.position);

		gridsDone.Add (gridNumber);

	}

	public void ClaimMap(int gridClaimed, int team){
	
		claimedObj = (GameObject)Instantiate (claimedImage, Vector3.zero, Quaternion.Euler (0, 0, 0));
		claimedObj.transform.SetParent (minimapCanvas.transform, false);

		Destroy (curSigil.gameObject);
		curSigil = null;
	
	}

}
