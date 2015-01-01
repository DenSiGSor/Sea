using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public bool playerInShip = false;
	private bool shipUnderControl = false;

	//Переменные движения 
	enum Sail{
		NoSail,
		HalfSail,
		FullSail
	};
	Sail shipSail = Sail.NoSail;
	
	public Vector3 moveDirection;
	float sailStatus = 0.0F;
	float maxSpeed = 10.0F;
	float draft;
	float windSpeed = 1.0F;
	float inertion = 1F;
	public float currentSpeed = 0;
	
	float curSpped;

	// Use this for initialization
	void Start () {
		moveDirection = transform.TransformDirection (Vector3.forward);

	
	}
	
	// Update is called once per frame
	void Update () {
		
		moveDirection = transform.TransformDirection (Vector3.forward);

		if (Input.GetKeyDown (KeyCode.F) && shipUnderControl) {
			Debug.Log("LostControl");
			shipUnderControl = false;
			GetComponent<ShipPlayerControl>().enabled = false;
			GameObject.Find("Player").GetComponent<PlayerController>().TakeControl();
		}
		if (sailStatus == 0)
			inertion = inertion/2F;
		else
			inertion = 1F;

		//Вечное движение
		currentSpeed =  Time.deltaTime * maxSpeed * windSpeed * sailStatus * inertion;
		gameObject.transform.position += moveDirection.normalized * currentSpeed;
		if (!shipUnderControl)
		{
			GameObject.Find("Player").transform.position += moveDirection.normalized * currentSpeed;
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Enter in SanJuan");
		playerInShip = true;
	}
	
	void OnTriggerExit(Collider other) {
		Debug.Log("Exit SanJuan");
		playerInShip = false;
	}

	public bool TakeControl(){		
		Debug.Log("TakeControl");
		GetComponent<ShipPlayerControl>().enabled = true;
		shipUnderControl = true;

		return true;
	}

	public void SailUp(){
		if (shipSail == Sail.NoSail){
			
			Debug.Log("Up Sail");

			shipSail = Sail.HalfSail;
			sailStatus = 0.5F;
			return;
		}
		if (shipSail == Sail.HalfSail){
			
			Debug.Log("Up Sail");

			shipSail = Sail.FullSail;
			sailStatus = 1F;	
			return;
		}
		if (shipSail == Sail.FullSail){
			
			Debug.Log("Sail Full");
			return;
		}
	}

	public void SailDown(){
		if (shipSail == Sail.FullSail){
			shipSail = Sail.HalfSail;
			sailStatus = 0.5F;
			return;
		}
		if (shipSail == Sail.HalfSail){
			shipSail = Sail.NoSail;
			sailStatus = 0F;	
			return;
		}
		if (shipSail == Sail.FullSail){
			return;
		}
	}

	public void MoveLeft(){
		transform.Rotate(0, - currentSpeed * 50 * Time.deltaTime, 0, Space.World);
	}
	
	public void MoveRight(){
		transform.Rotate(0, currentSpeed * 50 * Time.deltaTime, 0, Space.World);
	}

	public void TestFunc(){

	}
}
