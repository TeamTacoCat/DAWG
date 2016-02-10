using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool grounded = true;

	public GameObject sigil;
	public float sigilProg = 0;

	//This is the current power up that the player has available.
	private int powerUp = -1;
	private float mSpeed = 60;


	public bool fill=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		grounded = GetComponentInChildren<GroundCheck> ().ground;
		if (sigil == null) {
		
			sigilProg = 0;
		
		}

		if (powerUp != -1) {
			if(Input.GetKey(KeyCode.Z)){
				switch (powerUp) {
				case 0:
					this.GetComponent<PlayerController> ().SetMaxSpeed (mSpeed);
					StartCoroutine ("timer", 10.0f);
					break;
				default:
					break;
				}
				powerUp = -1;
			}
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

	IEnumerator timer(float T){
		yield return new WaitForSeconds (T);
		GetComponent<PlayerController> ().SetMaxSpeed (30);
	}

	public IEnumerator FillProgBar(){

		print ("Fill Progress started");

		while (sigilProg < 100) {
		
			sigilProg++;
			yield return new WaitForFixedUpdate ();
		
		
		}

		print ("Progress bar filled");



	}

}