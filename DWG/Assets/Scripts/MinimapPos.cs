using UnityEngine;
using System.Collections;

public class MinimapPos : MonoBehaviour {

	public Vector2[] GridPositions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector2 GetGridPos(int gridNum){
	
		return GridPositions [gridNum - 1];
	
	}
}
