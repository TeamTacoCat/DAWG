using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool grounded = true;

	public GameObject sigil;
	public float sigilProg = 0;

	//This is the current power up that the player has available.
	private int powerUp;

	public bool fill=false;

	public int teamNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		grounded = GetComponentInChildren<GroundCheck> ().ground;
		if (sigil == null) {
		
			sigilProg = 0;
		
		}


	
	}

	public void SetPowerUp(int pUp){
		powerUp = pUp;
	}

	public void Interact(){

		if (sigil != null) {
		
			StartCoroutine ("FillProgBar");
		
		} else {
		
			print ("not in sigil range");
		
		}

	}

	public IEnumerator FillProgBar(){

		print ("Fill Progress started");

		while (sigilProg < 100) {
		
			sigilProg++;
			yield return new WaitForFixedUpdate ();
		
		
		}

		print ("Progress bar filled");

		sigil.GetComponent<Sigil> ().Claim (teamNum);
		GameManager.AddPoints (teamNum);

	}

}