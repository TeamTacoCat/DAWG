using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]private float maxSpeed;
	[SerializeField]private float accel;
	[SerializeField]private float rotAccel;

	[SerializeField]private float jumpHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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

		if (Input.GetButtonDown ("Jump")) {

			float force = Mathf.Sqrt(2*Physics.gravity.y*-1*jumpHeight);
			print("Jumpforce:  "+force);
			//GetComponent<Rigidbody>().AddForce(Vector3.up*force);
			GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, force, GetComponent<Rigidbody>().velocity.z);

		}
	
	}
}
