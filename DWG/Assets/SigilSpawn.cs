using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SigilSpawn : MonoBehaviour {

	[SerializeField]private GameObject[] grids;
	[SerializeField]private GameObject minimap;
	private int sigilsSpawned;

	[SerializeField]private GameObject sigil;
	private GameObject curSigil;

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

		print ("Sigil spawned at:" + curSigil.transform.position);

		gridsDone.Add (gridNumber);

	}

}
