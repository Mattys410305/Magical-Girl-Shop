using UnityEngine;
using System.Collections;

public class SpringFollowCamera : MonoBehaviour {
	
	public Transform target;
	public float distance = 4.0f;
	public float height = 1.0f;
    public float lookatHeight = 1.0f;
    public float smoothLag = 0.2f;
	public float maxSpeed = 10.0f;
	public float snapLag = 0.3f;
	public float clampHeadPositionScreenSpace = 0.75f;
	public LayerMask lineOfSightMask = 0;

	private Vector3 headOffset = Vector3.zero;
	private Vector3 centerOffset = Vector3.zero;
	private PlayerController controller;
	private Vector3 velocity = Vector3.zero;
	private float targetHeight = 100000.0f;

	private Vector3 mousePos = Vector3.zero;
	private float mouseMoveSpeed = 0.0f;

	private bool isReleaseCursor = false;
	private bool isPause = false;


	void Awake ()
	{
		CharacterController characterController = (CharacterController)target.GetComponent<Collider>();
		if (characterController)
		{
			centerOffset = characterController.bounds.center - target.position;
			headOffset = centerOffset;
			headOffset.y = characterController.bounds.max.y - target.position.y;
		}
		
		if (target)
		{
			controller = target.GetComponent<PlayerController>();
		}
		
		if (!controller)
			Debug.Log("Please assign a target to the camera that has a Tank Controller script component.");
		mousePos = Input.mousePosition;
	}

	void LateUpdate () 
	{
		if(isPause == true) return;

		if(!target) return;
		Vector3 targetCenter = target.position + centerOffset;
		Vector3 targetHead = target.position + headOffset;

		targetHeight = targetCenter.y + height;

		ApplyPositionDamping (new Vector3(targetCenter.x, targetHeight, targetCenter.z));
		SetUpRotation(targetCenter, targetHead);
	}
	
	void ApplyMouseRotate(Vector3 targetCenter)
	{
		
		if(isReleaseCursor)
		{
			Cursor.lockState = CursorLockMode.None;
			//Screen.lockCursor = false;
			Cursor.visible = true;
			//target.SendMessage("setControllable", false);
			return;
		}

		//target.SendMessage("setControllable", true);
		Cursor.visible = false;


		if(mousePos.x <= (Screen.width/2 - 250.0f) || mousePos.x >= (Screen.width/2 + 250.0f))
		{
			transform.RotateAround(targetCenter, Vector3.up, mouseMoveSpeed);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.None;
			//Screen.lockCursor = true;
			//Screen.lockCursor = false;
			mousePos = Input.mousePosition;
			return;
		}
		else
		{
			transform.RotateAround(targetCenter, Vector3.up, mouseMoveSpeed);
			mouseMoveSpeed = (Input.mousePosition.x - mousePos.x)/4.0f;
			mousePos = Input.mousePosition;
		}
	}

	void ApplyPositionDamping (Vector3 targetCenter)
	{
		// We try to maintain a constant distance on the x-z plane with a spring.
		// Y position is handled with a seperate spring
		Vector3 position = transform.position;
		Vector3 offset = position - targetCenter;
		offset.y = 0;
		Vector3 newTargetPos = targetCenter + offset.normalized * distance;
		
		Vector3 newPosition;
		newPosition.x = Mathf.SmoothDamp(position.x, newTargetPos.x, ref velocity.x, smoothLag, maxSpeed);
		newPosition.z = Mathf.SmoothDamp(position.z, newTargetPos.z, ref velocity.z, smoothLag, maxSpeed);
		newPosition.y = Mathf.SmoothDamp(position.y, targetCenter.y, ref velocity.y, smoothLag, maxSpeed);


		transform.position = newPosition;

		//ApplyMouseRotate(targetCenter);

	}
	
	void SetUpRotation (Vector3 centerPos,Vector3 headPos)
	{
		Vector3 cameraPos = transform.position;
		Vector3 offsetToCenter = centerPos - cameraPos;

		// Generate base rotation only around y-axis
		Quaternion yRotation = Quaternion.LookRotation(new Vector3(offsetToCenter.x, lookatHeight, offsetToCenter.z));
		
		Vector3 relativeOffset = Vector3.forward * distance + Vector3.down * height;
		transform.rotation = yRotation * Quaternion.LookRotation(relativeOffset);
        
	}

	public void lockCursor()
	{
		isReleaseCursor = false;
	}

	public void unlockCursor()
	{
		isReleaseCursor = true;
	}

	public void setPause(bool pause)
	{
		isPause = pause;
		mousePos = Input.mousePosition;
		mouseMoveSpeed = 0;
	}
}
