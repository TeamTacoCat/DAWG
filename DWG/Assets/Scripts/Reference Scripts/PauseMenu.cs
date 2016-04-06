/*using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject PauseUI;
	public GameObject[] PauseOptions;
	public GameObject selectorP;
	private bool paused = false;
	public int selected;
	public bool axisPressed;
	public GameObject roundcontrol;

	void Start(){

		PauseUI.SetActive (false);
		selectorP.SetActive (true);
		selected = 0;
		selectionEffect ();
		axisPressed = false;

		foreach (SpriteRenderer s in this.GetComponentsInChildren<SpriteRenderer>()) {
		
			s.GetComponent<Renderer>().sortingOrder = 10;
		
		}
	}

	void Update(){
		pausePressed ();
		if (axisPressed == false && paused) {
			if (Input.GetAxisRaw ("MenuDPad") < 0) {
				
				if (selected >= 1) {
					
					selected = 0;
					
				} else {
					selected++;
				}
				
				selectionEffect ();
				axisPressed = true;
				
			} else if (Input.GetAxisRaw("MenuDPad") > 0) {
				
				if (selected <= 0) {
					
					selected = 1;

				} else {
					selected--;
				}
				
				selectionEffect ();
				axisPressed = true;
				
			}
		}
		
		if (Mathf.Approximately(Input.GetAxisRaw ("MenuDPad"),0.0f)) {
			
			axisPressed = false;
			
		}
		
		if(Input.GetButtonDown("Submit") && paused){
			
			selectOption ();
			
		}

	}
	void pausePressed(){

		print ("a");
		if (roundcontrol.GetComponent<Round> ().roundStarted == true) {
			print("b");
			if (Input.GetButtonDown ("Pause")) {
				print ("c");
		
				paused = !paused;

				if (paused) {
					//selected=1;
					PauseUI.SetActive (true);
					Time.timeScale = 0;
					
				}
				
				if (!paused) {
					selected = 0;
					PauseUI.SetActive (false);
					Time.timeScale = 1;
					
					
				}
		
			}
	

		}
	}
	void selectOption(){
		
		switch (selected) {

		case 0:
			paused=!paused;
			selected = 0;
			PauseUI.SetActive (false);
			Time.timeScale = 1;
			break;
		case 1:
			Time.timeScale = 1;
			GameManager.ChooseLevel ("MainMenu");
			break;
		default:
			break;
			
		}
		
	}
	void selectionEffect ()
	{
		selectorP.transform.position = PauseOptions [selected].transform.position;
	}
}
*/