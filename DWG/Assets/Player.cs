using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//This is the current power up that the player has available.
	private int powerUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPowerUp(int pUp){
		powerUp = pUp;
	}
}