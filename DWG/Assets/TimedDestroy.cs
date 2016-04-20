using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {

	public float DestroyTime{ get; set; }

	// Use this for initialization
	void Start () {

		if (DestroyTime > 0) {
		
			Invoke ("Kill", DestroyTime);
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Kill(){

		Destroy (this.gameObject);

	}

	public IEnumerator DestroyAfter(float time){
	
		yield return new WaitForSeconds (time);

		Destroy (this.gameObject);
	
	}

}
