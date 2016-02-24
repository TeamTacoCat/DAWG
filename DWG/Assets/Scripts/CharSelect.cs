﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour {

	public bool PlayerActive{ get; set; }
	public bool PlayerReady{ get; set; } 

	// Use this for initialization
	void OnEnable () {

		GetComponent<Image> ().color = Color.white;
		PlayerActive = false;
		PlayerReady = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (PlayerActive) {

			GetComponent<Image> ().color = Color.blue;

		}
		if (PlayerReady) {
		
			GetComponent<Image> ().color = Color.red;
		
		}
	
	}


}
