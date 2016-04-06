using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {

	public AudioClip[] clip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
		
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){

				print("Raycast hit:"+hit.collider.gameObject.name);

				if(hit.collider.gameObject.name == "SoundTest"){

					print("Raycast hit sound target");

					SFX.sound.PlaySound(clip[Random.Range(0,clip.Length)]);


				}

			}
		
		}
	
	}
}
