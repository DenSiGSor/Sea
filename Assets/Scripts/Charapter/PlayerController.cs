using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public KeyCode SwitchCode;
	private Vector3 spawnPoint = new Vector3 (0, 5.2F, 0);

	Vector3 saveCoord;
	Vector3 saveRotation;
	GameObject ControlledShip;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//Кнопка переключения на корабль
		if (Input.GetKeyDown(SwitchCode)){
			StopControl();
		}
	}

	//Убираем контроль игрока и включаем корабль
	public void StopControl(){
		Debug.Log("stop");

		GameObject[] ships;
		ships = GameObject.FindGameObjectsWithTag("Ship");
		foreach (GameObject ship in ships) {
			if (ship.GetComponent<ShipController> ().playerInShip){
				//Здесь должна быть проверка на права

				ControlledShip = ship;
//				saveCoord = ControlledShip.transform.position;
//				saveRotation = ControlledShip.transform.InverseTransformDirection(spawnPoint + ControlledShip.transform.position); 
				PushToInactive();

				ControlledShip.GetComponent<ShipController> ().TakeControl();
			}
		}
	}

	public void TakeControl(){
		PushToАctive ();
	}

	private void PushToInactive(){
		MonoBehaviour[] AllComp = GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour comp in AllComp){
			comp.enabled = false;
		}
		gameObject.transform.position = new Vector3(0,-1000,0);
	}

	private void PushToАctive(){
		MonoBehaviour[] AllComp = GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour comp in AllComp){
			comp.enabled = true;
		}
		gameObject.transform.position = ControlledShip.transform.position + spawnPoint;
	}
}
