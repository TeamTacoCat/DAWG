using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	[SerializeField]private float maxSpeed;
	[SerializeField]private float accel;
	[SerializeField]private float jumpHeight;

	public GameObject cam;
	public GameObject look;

	public int playerNum;

	public RectTransform fuelTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private int currentFuel;

	[SerializeField]private int fuelRegen = 1;
	[SerializeField]private float fuelRegenTime = .05f;
	[SerializeField]private int jumpCost = 20;
	[SerializeField]private int dashCost = 20;

	private GameObject dashboox;

	private int CurrentFuel {
		get { return currentFuel; }
		set { 
			currentFuel = value;
			HandleFuel ();
		}
	}

	public int maxFuel;
	public Text fuelText;
	public Image visualFuel;

	// Use this for initialization
	void Start ()
	{

		look = GameObject.Find ("Look" + playerNum.ToString ());

		cachedY = fuelTransform.position.y;
		maxXValue = fuelTransform.position.x;
		minXValue = fuelTransform.position.x - fuelTransform.rect.width;
		currentFuel = maxFuel;

		dashCost = 20;
		jumpCost = 20;

		foreach (Transform t in GetComponentsInChildren<Transform>()) {

			if (t.gameObject.name == "DashBox") {

				dashboox = t.gameObject;
				dashboox.SetActive (false);

			}

		}
	
		StartCoroutine ("FuelRegen");
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Time.timeScale != 0) {


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

			//HORIZONTAL MOVEMENT


			//Vector3 lookDir = transform.position + transform.TransformDirection (((zDir * (z / Mathf.Abs (z))) + cam.transform.right).normalized);


			//transform.LookAt (transform.TransformDirection (transform.right));
			//transform.LookAt(cam.transform.position);

			/*
		if (Input.GetAxis ("Horizontal") != 0) {
		
			transform.Rotate(0f, rotAccel*Input.GetAxis("Horizontal"), 0f);
		
		}
		if (Input.GetAxis ("Vertical") != 0) {

			float locVel = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z;
		
			if(locVel < maxSpeed && Input.GetAxis("Vertical")>0){

				//GetComponent<Rigidbody>().AddForce(0,0,accel*Input.GetAxis("Vertical"));
				GetComponent<Rigidbody>().AddForce(transform.forward*accel*Input.GetAxis ("Vertical"));

			}else if(locVel > (-1f*maxSpeed) && Input.GetAxis("Vertical")<0){

				//GetComponent<Rigidbody>().AddForce(0,0,accel*Input.GetAxis("Vertical"));
				GetComponent<Rigidbody>().AddForce(transform.forward*accel*Input.GetAxis ("Vertical"));

			}

//			else{
//
//				Vector3 locVelV3 = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);
//				locVelV3.z = maxSpeed*(Input.GetAxis("Vertical")/Mathf.Abs(Input.GetAxis("Vertical")));
//
//				GetComponent<Rigidbody>().velocity = locVelV3;
//
//					//transform.forward*maxSpeed*(Input.GetAxis("Vertical")/Mathf.Abs(Input.GetAxis("Vertical")));
//
//			}

			print("Velocity:  "+locVel);
		
		}
	*/
			/*
		if(Input.GetKey("camUp")){

			//camBoom2.transform.Rotate (10, 0, 0);
			cam.transform.RotateAround (this.transform.position, cam.transform.right, 5);
			//camBoom.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y, this.transform.position.z);


		}if(Input.GetKey("camDown")){

			//camBoom2.transform.Rotate (-10, 0, 0);
			cam.transform.RotateAround (this.transform.position, cam.transform.right, -5);
			//camBoom.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y, this.transform.position.z);


		}if(Input.GetKey("camRight")){

			//camBoom.transform.Rotate (0, -10, 0);
			//cam.transform.RotateAround (camBoom.transform.position, Vector3.down, 5);
			cam.transform.RotateAround (this.transform.position, transform.up, -5);


		}if(Input.GetKey("camLeft")){

			//camBoom.transform.Rotate (0, 10, 0);
			//cam.transform.RotateAround (camBoom.transform.position, Vector3.up, 5);
			cam.transform.RotateAround (this.transform.position, transform.up, 5);
		}
		*/
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
		//GetComponent<Rigidbody>().AddForce(Vector3.up*force);
		if (GetComponent<Player> ().grounded) {
			//GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, force, GetComponent<Rigidbody> ().velocity.z);
			GetComponent<Rigidbody> ().AddForce (0, force, 0, ForceMode.VelocityChange);
		} else if (currentFuel >= jumpCost) {
			CurrentFuel -= jumpCost;
			GetComponent<Rigidbody> ().AddForce (0, force, 0, ForceMode.VelocityChange);
		}
	}

	//Current speed power up here
	public void SetMaxSpeed (float mSpeed)
	{
		maxSpeed = mSpeed;
	}

	public void resetMaxSpeed (float mSpeed)
	{
		maxSpeed = 30;
	}

	//This handles the fuel bar on the HUD.
	private void HandleFuel ()
	{
		fuelText.text = "Fuel: " + currentFuel;

		float currentXValue = MapValues (currentFuel, 0, maxFuel, minXValue, maxXValue);

		fuelTransform.position = new Vector3 (currentXValue, cachedY);
	}

	private float MapValues (float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMax;
	}

	//With this, the fuel gauge always regenerates at the rate set by "fuelRegen" whenever it isn't full.
	IEnumerator FuelRegen ()
	{
		bool loop = true;
		while (loop) {
			if (CurrentFuel < maxFuel) {
				CurrentFuel += fuelRegen;
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

			StartCoroutine ("DashEnd");
		
		}
	
	}

	IEnumerator DashEnd ()
	{
	
		yield return new WaitForSeconds (.2f);
		GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity / 10f;
		dashboox.SetActive (false);

	}

	//This is the Fuel restore power up as described in the GDD. With this, fuel is maxed out instantly.
	public void FuelRestore ()
	{
		if (CurrentFuel < maxFuel)
			CurrentFuel = maxFuel;
	}


	public void Discombobulate ()
	{
		switch (this.GetComponent<Player> ().teamNum) {

		case 1:
			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;

			StartCoroutine ("dTimer");

			break;

		case 2:
			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;

			StartCoroutine ("dTimer");
			break;

		case 3:
			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = false;

			StartCoroutine ("dTimer");
			break;

		case 4:
			GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = false;
			GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = false;

			StartCoroutine ("dTimer");
			break;

		default:
			break;

		}
	}

	IEnumerator dTimer ()
	{
		yield return new WaitForSeconds (10.0f);
		GameObject.Find ("MinimapArrow1").GetComponent<Image> ().enabled = true;
		GameObject.Find ("MinimapArrow2").GetComponent<Image> ().enabled = true;
		GameObject.Find ("MinimapArrow3").GetComponent<Image> ().enabled = true;
		GameObject.Find ("MinimapArrow4").GetComponent<Image> ().enabled = true;
	}
}