using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {

		switch (player.GetComponent<Player> ().teamNum) {

		case 1:
			GetComponent<Image> ().color = Color.red;
			break;
		case 2:
			GetComponent<Image> ().color = Color.blue;
			break;
		case 3:
			GetComponent<Image> ().color = Color.green;
			break;
		case 4:
			GetComponent<Image> ().color = Color.yellow;
			break;

		default:
			break;

		}
	
	}
	
	// Update is called once per frame
	void Update () {

		PosMatch ();
	
	}

	void PosMatch(){

		float x = 0;
		float y = 0;

		if(player.transform.position.x >0){

			x = (player.transform.position.x * 113) / 540;

		}else{

			x = (player.transform.position.x * -123) / -540;

		}
		if(player.transform.position.y >0){

			y = (player.transform.position.z * 113) / 540;

		}else{

			y = (player.transform.position.z * -123) / -540;

		}
		transform.localPosition = new Vector2 (x, y);
		transform.rotation = Quaternion.Euler(0, 0, -1*player.transform.rotation.eulerAngles.y);
		transform.SetAsLastSibling ();

	}

}
