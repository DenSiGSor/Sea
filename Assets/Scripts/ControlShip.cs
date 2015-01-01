using UnityEngine;
using System.Collections;

public class ControlShip : MonoBehaviour {

	private Vector3 savePosition;
	private bool controlBoat = false;
	public KeyCode SwitchCode;
	
	private PlayerController playerController;

//	public Transform camera;

	private GameObject Ship;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controlBoat)
			return;
		if (Input.GetKey(SwitchCode)){
			controlBoat = true;
			//playerController.StopControl();
		}

	}



}
