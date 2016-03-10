using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float maxSpeed{ get; set; }
	[SerializeField]private float accel;
	[SerializeField]private float jumpHeight;

	public GameObject cam;
	public GameObject look;

	public int playerNum;

	public RectTransform fuelTransform;
//	private float cachedY;
//	private float minXValue;
//	private float maxXValue;
	public float currentFuel{ get; set; }

	[SerializeField]private int fuelRegen = 1;
	[SerializeField]private float fuelRegenTime = .05f;
	[SerializeField]private int jumpCost = 20;
	[SerializeField]private int dashCost = 20;

	private GameObject dashboox;
	private GameObject[] players = new GameObject[4];
	private GameObject[] arrows = new GameObject[4];

	public float maxFuel{ get; set; }
	public Text fuelText;
	public Image visualFuel;

	private Animator anim;

	// Use this for initialization
	void Start ()
	{

		//look = GameObject.Find ("Look" + playerNum.ToString ());
		maxFuel = 100;
		maxSpeed = 30;
		anim = GetComponentInChildren<Animator> ();

//		cachedY = fuelTransform.position.y;
//		maxXValue = fuelTransform.position.x;
//		minXValue = fuelTransform.position.x - (fuelTransform.rect.width);
		currentFuel = maxFuel;
		HandleFuel ();

		dashCost = 20;
		jumpCost = 33;

		foreach (Transform t in GetComponentsInChildren<Transform>()) {

			if (t.gameObject.name == "DashBox") {

				dashboox = t.gameObject;
				dashboox.SetActive (false);

			}

		}

		for (int i = 0; i < players.Length; i++) {
		
			if(GameObject.Find ("Player" + (i + 1).ToString ())){
				players [i] = GameObject.Find ("Player" + (i + 1).ToString ());
			}
		
		}

		for (int i = 0; i < arrows.Length; i++) {

			if(GameObject.Find ("Canvas"+GameObject.Find("GameManager").GetComponent<GameManager>().curMatch.NumPlayers.ToString()+"/MinimapArrow" + (i + 1).ToString ())){
				arrows [i] = GameObject.Find ("Canvas"+GameObject.Find("GameManager").GetComponent<GameManager>().curMatch.NumPlayers.ToString()+"/MinimapArrow" + (i + 1).ToString ());
			}

		}
	
		StartCoroutine ("FuelRegen");
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Time.timeScale != 0) {

			anim.SetFloat ("vertVel", GetComponent<Rigidbody> ().velocity.y);
			anim.SetBool ("grounded", GetComponent<Player> ().grounded);
			anim.SetFloat ("horizVel", (GetComponent<Rigidbody> ().velocity.x + GetComponent<Rigidbody> ().velocity.z));

			HorizontalMovement ();
			LookDirection ();
			if (Input.GetButtonDown ("Jump" + playerNum.ToString ())) {
				Jump ();
			}
			if (Input.GetButtonDown ("Interact" + playerNum.ToString ())) {
		
				GetComponent<Player> ().Interact ();

		
			} else if (Input.GetButtonUp ("Interact" + playerNum.ToString ()) && GetComponent<Player> ().sigil != null) {
		
				GetComponent<Player> ().StopCoroutine ("FillProgBar");
		
			}

			if (Input.GetButtonDown ("Dash" + playerNum.ToString ())) {
		
				Dash ();
		
			}
		}
	}

	void HorizontalMovement ()
	{
	
		float x = Input.GetAxis ("Horizontal" + playerNum.ToString ()) * accel;
		float z = Input.GetAxis ("Vertical" + playerNum.ToString ()) * accel;

		Vector3 zDir = (transform.position - cam.transform.position).normalized;
		zDir.y = 0;

		zDir = Vector3.Scale (zDir, new Vector3 (1000, 1000, 1000));
		zDir = Vector3.ClampMagnitude (zDir, 1);

		Vector3 horizVel = GetComponent<Rigidbody> ().velocity;
		horizVel.y = 0;

		if (Input.GetAxis("Horizontal"+ playerNum.ToString ()) != 0 || Input.GetAxis ("Vertical" + playerNum.ToString ()) != 0 ){
		
			anim.SetBool ("horizAccel", true);
		
		}else{

			anim.SetBool("horizAccel", false);

		}

		if (horizVel.magnitude < maxSpeed) {
			GetComponent<Rigidbody> ().AddForce (zDir * z, ForceMode.Force);
			GetComponent<Rigidbody> ().AddForce (cam.transform.right * x, ForceMode.Force);
		}
	
	}

	void LookDirection ()
	{
	
		look.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		look.transform.position = look.transform.position + (cam.transform.right * Input.GetAxis ("Horizontal" + playerNum.ToString ())) + (cam.transform.forward * Input.GetAxis ("Vertical" + playerNum.ToString ()));
		look.transform.position = new Vector3 (look.transform.position.x, transform.position.y, look.transform.position.z);

		if (look.transform.position != this.transform.position) {
			transform.LookAt (look.transform.position);
		}
	
	}

	void Jump ()
	{

		float force = Mathf.Sqrt (2 * Physics.gravity.y * -1 * jumpHeight);
		if (GetComponent<Player> ().grounded) {
			//GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, force, GetComponent<Rigidbody> ().velocity.z);
			GetComponent<Rigidbody> ().AddForce (0, force, 0, ForceMode.VelocityChange);
			anim.SetBool ("jump", true);
		} else if (currentFuel >= jumpCost) {
			if (GetComponent<Rigidbody> ().velocity.y < 0) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, 0, GetComponent<Rigidbody> ().velocity.z);
			}
			currentFuel -= jumpCost;
			GetComponent<Rigidbody> ().AddForce (0, force, 0, ForceMode.VelocityChange);
			anim.SetBool ("jump", true);
		}
	}

	//Current speed power up here
	public void SetMaxSpeed(float mSpeed){
		print ("Speed up triggered");
		maxSpeed = mSpeed;
	}

	public void resetMaxSpeed(float mSpeed){
		maxSpeed = 30;
	}

	//This handles the fuel bar on the HUD. 
	private void HandleFuel(){

		//fuelTransform.parent.transform.localScale = new Vector3((currentFuel / maxFuel), 1, 1);
//		fuelText.text = "Fuel: " + currentFuel;

		//float currentXValue = MapValues (currentFuel, 0, maxFuel, minXValue, maxXValue);

		//fuelTransform.position = new Vector3 (currentXValue, cachedY);
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMax;
	}

	//With this, the fuel gauge always regenerates at the rate set by "fuelRegen" whenever it isn't full.
	IEnumerator FuelRegen ()
	{
		bool loop = true;
		while (loop) {
			if(GetComponent<Player>().grounded){
			if (currentFuel < maxFuel) {
				currentFuel += fuelRegen;
			}
			}
			yield return new WaitForSeconds (fuelRegenTime);
		}
	}

	void Dash ()
	{
	
		if (currentFuel > dashCost) {
		
			currentFuel -= dashCost;

			float x = Input.GetAxis ("Horizontal" + playerNum.ToString ()) * accel;
			float z = Input.GetAxis ("Vertical" + playerNum.ToString ()) * accel;

			Vector3 zDir = (transform.position - cam.transform.position).normalized;
			zDir.y = 0;

			zDir = Vector3.Scale (zDir, new Vector3 (1000, 1000, 1000));
			zDir = Vector3.ClampMagnitude (zDir, 1);

			Vector3 horizVel = GetComponent<Rigidbody> ().velocity;
			horizVel.y = 0;

			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().AddForce (zDir * z * 3f, ForceMode.VelocityChange);
			GetComponent<Rigidbody> ().AddForce (cam.transform.right * x * 3f, ForceMode.VelocityChange);

			dashboox.SetActive (true);
			anim.SetTrigger ("dash");

			StartCoroutine ("DashEnd");
		
		}
	
	}

	IEnumerator DashEnd ()
	{
	
		yield return new WaitForSeconds (.15f);
		GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity / 10f;
		dashboox.SetActive (false);

	}

	//This is the Fuel restore power up as described in the GDD. With this, fuel is maxed out instantly.
	public void FuelRestore(){
		print ("FuelRestore triggered");
		if (currentFuel < maxFuel)
			currentFuel = maxFuel;
	}


	public void Discombobulate(){
		print ("Discombobulate triggered");
		if (arrows [0] != null) {
			if (arrows [0].GetComponent<Arrow> ().player.GetComponent<Player>().teamNum != GetComponent<Player> ().teamNum) {
				arrows [0].GetComponent<Image> ().enabled = false;
			}
		}
		if (arrows [1] != null) {
			if (arrows [1].GetComponent<Arrow> ().player.GetComponent<Player>().teamNum != GetComponent<Player> ().teamNum) {
				arrows [1].GetComponent<Image> ().enabled = false;
			}
		}
		if (arrows [2] != null) {
			if (arrows [2].GetComponent<Arrow> ().player.GetComponent<Player>().teamNum != GetComponent<Player> ().teamNum) {
				arrows [2].GetComponent<Image> ().enabled = false;
			}
		}
		if (arrows [3] != null) {
			if (arrows [3].GetComponent<Arrow> ().player.GetComponent<Player>().teamNum != GetComponent<Player> ().teamNum) {
				arrows [3].GetComponent<Image> ().enabled = false;
			}
		}
		StartCoroutine ("dTimer");
//		switch (this.GetComponent<Player> ().teamNum){
//
//		case 1:
//			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;
//
//			StartCoroutine ("dTimer");
//
//			break;
//
//		case 2:
//			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;
//
//			StartCoroutine ("dTimer");
//			break;
//
//		case 3:
//			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;
//
//			StartCoroutine ("dTimer");
//			break;
//
//		case 4:
//			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
//			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;
//
//			StartCoroutine ("dTimer");
//			break;
//
//		default:
//			break;
//
//		}
	}

	IEnumerator dTimer(){
		yield return new WaitForSeconds (10.0f);
		foreach (GameObject p in arrows) {
			if (p != null) {
				p.GetComponent<Image> ().enabled = true;
			}
		}
	}

	public void Grounded(){
		print ("Grounded triggered");
		if (players [0] != null) {
			if (players [0].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
				players [0].GetComponent<PlayerController> ().currentFuel = 0;
			}
		}
			if (players [1] != null) {
				if (players [1].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
					players [1].GetComponent<PlayerController> ().currentFuel = 0;
				}
			}
			if (players [2] != null) {
				if (players [2].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
					players [2].GetComponent<PlayerController> ().currentFuel = 0;
				}
			}
			if (players [3] != null) {
				if (players [3].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
					players [3].GetComponent<PlayerController> ().currentFuel = 0;
				}
			}
	}

	public void SpeedCutter(){
		print ("SpeedCutter triggered");
		if (players [0] != null) {
			if (players [0].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
				players [0].GetComponent<PlayerController> ().maxSpeed = 15;
			}
		}
		if (players [1] != null) {
			if (players [1].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
				players [1].GetComponent<PlayerController> ().maxSpeed = 15;
			}
		}
		if (players [2] != null) {
			if (players [2].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
				players [2].GetComponent<PlayerController> ().maxSpeed = 15;
			}
		}
		if (players [3] != null) {
			if (players [3].GetComponent<Player> ().teamNum != GetComponent<Player> ().teamNum) {
				players [3].GetComponent<PlayerController> ().maxSpeed = 15;
			}
		}
		StartCoroutine ("scTimer");
	}

	IEnumerator scTimer (){
		yield return new WaitForSeconds (10.0f);
		foreach (GameObject p in players) {
		
			if (p != null) {
				p.GetComponent<PlayerController> ().maxSpeed = 30;
			}
		}
	}
}