using UnityEngine;
using System.Collections;

public class MusicSingleton : MonoBehaviour {

	public static GameObject mInstance;
	[SerializeField]private AudioClip[] songs;
	private AudioSource source;

	// Use this for initialization
	void Start () {

		if (mInstance) {
			
			DestroyImmediate (gameObject);
			
		} else {
			
			DontDestroyOnLoad (gameObject);
			mInstance = this.gameObject;
			source = GetComponent<AudioSource> ();
			if (source) {
			
				print ("Source referenced");

			}

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnLevelWasLoaded(){

		source = GetComponent<AudioSource> ();

		if (GameManager.curScene == "MainMenu") {

			if (source.isPlaying ) {
			
				source.Stop ();
			
			}
			source.clip = songs [0];
			source.Play ();
		
		}else if(GameManager.curScene == "LevelLayout" || GameManager.curScene == "SinglePlayerLevel"){

			//if (source.clip != songs [1]) {
			
			source.Stop ();
			source.clip = songs [Random.Range(1, 3)];
			source.Play ();
			
			//}

		}

	}

//	public static void ChangeSong(int songNum){
//	
//		source.Stop ();
//		source.clip = songs [songNum];
//		source.Play ();
//	
//	}

}
