using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target; // what object we will be following
    public float xOffset;
    public float yOffset;
    public float distance;
    public float followSpeed = 1;
	public float horizontalWobble = 0; // horizontal rotation in the camera as the object moves left or right
	public float wobbleSpeed = 0;
	
	private Quaternion _lookRotation;
    private Vector3 _direction;
	private float startRotation;
	
	void Start()
	{
		startRotation = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		//Debug.Log(transform.rotation.y + "  vs  " + startRotation);
		Vector3 goToVector = new Vector3(target.transform.position.x + xOffset, target.transform.position.y + yOffset, target.transform.position.z + distance);
		transform.position = Vector3.Lerp(transform.position, goToVector, Time.deltaTime * followSpeed);
		/*
		if(goToVector.x < transform.position.x)
		{
			if(startRotation - Mathf.Abs(transform.rotation.y) < horizontalWobble)
			{
				transform.Rotate(Vector3.down * Time.deltaTime * wobbleSpeed);
			}
		}
		
		else
		{
			if(startRotation - transform.rotation.y > horizontalWobble*-1)
			{
				transform.Rotate(Vector3.up * Time.deltaTime * wobbleSpeed);
			}
		}
		*/
			
		/*if(horizontalWobble != 0)
		{
		   _direction = (goToVector - transform.position).normalized;
	
	       //create the rotation we need to be in to look at the target
	       _lookRotation = Quaternion.LookRotation(_direction*horizontalWobble);
	
	       //rotate us over time according to speed until we are in the required rotation
	       transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * wobbleSpeed);
		}*/
	}
	
	public void zoomIn()
	{
		animation.Play("CameraShift");
	}
	
	public void zoomOut()
	{
		animation.Stop();
		animation["CameraShift"].speed = -1;
		animation.Play("CameraShift");
	}
}
