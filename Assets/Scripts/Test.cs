using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public KeyCode test;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(test)){
			Debug.Log("ALARM!!!!");
		}
	}
}
