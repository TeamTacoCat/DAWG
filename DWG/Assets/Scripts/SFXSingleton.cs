using UnityEngine;
using System.Collections;

public class SFXSingleton : MonoBehaviour {

	public static GameObject mInstance;
	[SerializeField]private AudioClip[] SFX;

	// Use this for initialization
	void Start () {

		if (mInstance) {

			DestroyImmediate (gameObject);

		} else {

			DontDestroyOnLoad (gameObject);
			mInstance = this.gameObject;

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
