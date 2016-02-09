using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager GMInstance;

	// Use this for initialization
	void Start () {

		if (GMInstance) {
		
			DestroyImmediate (this);
		
		} else {
		
			GMInstance = this;
			Application.targetFrameRate = 60;
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {
		
			Application.Quit ();
		
		}

	
	}

	public void LoadScene(string sceneName){
	
		SceneManager.LoadScene (sceneName);
	
	}

	public void QuitGame(){
	
		Application.Quit ();
	
	}

}
