using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HiLiteEmbiggen : MonoBehaviour {

	private EventSystem e;

	// Use this for initialization
	void Start () {

		e = GameObject.Find ("EventSystem").GetComponent<EventSystem>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (e.currentSelectedGameObject == this.gameObject) {

			this.transform.localScale = new Vector3 (.402f, .402f);
		
		} else {
		
			this.transform.localScale = new Vector3 (.302f, .302f);
		
		}
	
	}
}
