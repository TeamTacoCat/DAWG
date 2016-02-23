using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool grounded = true;

	public GameObject sigil;
	public GameObject searcher;
	public float sigilProg = 0;

	//This is the current power up that the player has available.
	private int powerUp = -1;
	private float mSpeed = 60;

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
		
	IEnumerator timer(float T){
		yield return new WaitForSeconds (T);
		GetComponent<PlayerController> ().SetMaxSpeed (30);
	}

	public void SetPowerUp(int pUp){
		powerUp = pUp;
	}

	public void Interact(){

		if (sigil != null) {
			if (powerUp == 2)
				StartCoroutine ("SpellSlinger");
			else
				StartCoroutine ("FillProgBar");
		
		} 

		else {
		
			switch (powerUp) {
			case 0:
				this.GetComponent<PlayerController> ().SetMaxSpeed (mSpeed);
				StartCoroutine ("timer", 10.0f);
				break;
			case 1:
				this.GetComponent<PlayerController> ().FuelRestore ();
				break;
			case 3:
				this.GetComponent<PlayerController> ().Discombobulate();
				break;
			default:
				break;
			}
			powerUp = -1;
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

	public IEnumerator SpellSlinger(){

		print ("Fill Progress double speed");

		while (sigilProg < 100) {
			
			sigilProg += 2;
			yield return new WaitForFixedUpdate ();
		
		}

		sigil.GetComponent<Sigil> ().Claim (teamNum);
		GameManager.AddPoints (teamNum);

		powerUp = -1;
	}
}