using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private static GameManager GMInstance;
	private static int[] points = new int[4];

	// Use this for initialization
	void Start () {

		if (GMInstance) {
		
			DestroyImmediate (this);
		
		} else {
		
			GMInstance = this;
			DontDestroyOnLoad(this);
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
		if (sceneName == "LevelLayout") {
		
			for (int i = 0; i < points.Length; i++) {
			
				points [i] = 0;
			
			}
		
		}
	
	}

	public void QuitGame(){
	
		Application.Quit ();
	
	}

	public static void AddPoints(int teamNum){
	
		points [teamNum - 1]++;

		print ("Current points:");

		for(int i = 0;i<points.Length;i++){

			print("Team "+(i+1)+":"+points[i]);

		}
	
	}

}
