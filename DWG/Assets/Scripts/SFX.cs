using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFX : MonoBehaviour {

	private List<AudioSource> source = new List<AudioSource>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (source.Count > 0) {
		
			foreach (AudioSource s in source) {
		
				if (!s.isPlaying) {

					Destroy (s);

				}
		
			}

		}

	}

	public void PlaySound(AudioClip clip){

		AudioSource newSource = gameObject.AddComponent<AudioSource>();

		source.Add (newSource);

		newSource.clip = clip;
		newSource.Play ();

	}

}
