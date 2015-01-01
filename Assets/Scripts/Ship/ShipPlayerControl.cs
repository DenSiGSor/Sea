using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
[AddComponentMenu("Camera-Control/Mouse Look")]
public class ShipPlayerControl : MonoBehaviour {

//	public Transform Target = Camera.main.transform;
	private float distance = 50.0f;
	private float maxDistance = 120.0f;
	private float minDistance = 30.0f;
	private float xSpeed = 250.0f;
	private float ySpeed = 120.0f;
	private float yMinLimit = 5.0f;
	private float yMaxLimit = 70.0f;
	
	private float x;
	private float y;

	private float keyRot = 0f;

//	Vector3 angles;

	private ShipController controller;
//	GameObject Controller = shipController.GetComponent<ShipController>();
	// Use this for initialization
	void Awake()
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.x;
		y = angles.y;
		
		if(GetComponent<Rigidbody>() != null)
		{
			rigidbody.freezeRotation = true;
		}
	}

	void Start () {
		controller = GetComponent<ShipController>();

		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)){
			controller.SailUp();
		}

		if(Input.GetKeyDown(KeyCode.S)){
			controller.SailDown();
		}
		if(Input.GetKey(KeyCode.A)){
			controller.MoveLeft();

			keyRot = - controller.currentSpeed * 0.85f;
			return;		
		}else
			keyRot = 0f;
		
		if(Input.GetKey(KeyCode.D)){
			controller.MoveRight();
			
			keyRot = controller.currentSpeed * 0.85f;
			return;		
		}else
			keyRot = 0f;

		//Camera.main.transform.localPosition = gameObject.transform.TransformPoint(new Vector3 (0, 60F, -80F));
		//Vector3 direction = controller.moveDirection;
		//direction = Quaternion.Euler(25, 0, 0) * direction;
		//Camera.main.transform.rotation = Quaternion.LookRotation(direction);

	}
	void LateUpdate()
	{
		if(Camera.main.transform != null)
		{
			if (Input.GetAxis("Mouse ScrollWheel")!=0f){
				
				Debug.Log(distance + "  " +Input.GetAxis("Mouse ScrollWheel"));
				if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < maxDistance){
					distance -= Input.GetAxis("Mouse ScrollWheel") * 80;
				}else if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > minDistance){
					distance -= Input.GetAxis("Mouse ScrollWheel") * 80;
				}
			}
			x += (float)(Input.GetAxis("Mouse X") * xSpeed * 0.02f + keyRot);
			y -= (float)(Input.GetAxis("Mouse Y") * ySpeed * 0.02f);

			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -distance)) + transform.position;;
			
			Camera.main.transform.rotation = rotation;
			Camera.main.transform.position = position;
		}
	}
	
	private float ClampAngle(float angle, float min, float max)
	{
		if(angle < -360)
		{
			angle += 360;
		}
		if(angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp (angle, min, max);
	}
}
