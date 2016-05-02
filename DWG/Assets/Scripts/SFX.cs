using UnityEngine;
using System.Collections;

public class SFX : MonoBehaviour {

	public static SFX sound;

	// Use this for initialization
	void Start () {

		/*
		if (sound) {
			
			DestroyImmediate (gameObject);
			
		} else {
			
			DontDestroyOnLoad (gameObject);
			sound = this.gameObject;
			
		}*/
		if (sound) {
			
			DestroyImmediate (this);
			
		} else {
			
			DontDestroyOnLoad (gameObject);
			sound = this;
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	


	}

	public void PlaySound(AudioClip clip){

		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		newSource.clip = clip;
		newSource.volume = GameManager.sfxVolume;
		newSource.Play ();
		StartCoroutine ("EndClip", newSource);

	}

	IEnumerator EndClip(AudioSource s){

		while (s.isPlaying) {
		
			yield return null;
		
		}

		Destroy (s);

	}

}
