using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAttackClock : MonoBehaviour {

	private Text timerText;
	public float startTime{ get; set; }
	public float timeLeft{ get; set; }

	// Use this for initialization
	void Start () {

		startTime = 600.0f;
		timerText = this.GetComponent<Text> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		float t;

		if (startTime - Time.deltaTime <= 0f) {
		
			startTime = 0f;

		}else{
		
			startTime -= Time.deltaTime;
		
		}

		timeLeft = startTime;
		t = startTime;
			
		string minutes = ((int) t / 60).ToString();
		//string seconds = (t % 60).ToString ("f0");
		string milliseconds = (t % 60).ToString ("f2");

		timerText.text = "Time Remaining: " + minutes + "." + milliseconds;
		//timerText.text = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
	}
}
