/// <summary>
/// Drag tutorial.
/// Copyright 2011 revelopment.co.uk
/// Created by Carlos Revelo
/// 2011
/// </summary>

 
using UnityEngine;
using System.Collections;

public class CR_Drag : MonoBehaviour {

	public GameObject particle;
	
	[SerializeField]
	public float horizontalLimit = 2.5f, verticalLimit = 2.5f, dragSpeed = 0.1f;

	Transform cachedTransform;
	Vector3 startingPos;
	
	public CR_Snap snapTarget;
	public bool dragEnabled = true;
	
	private bool startDrag = false;
	
	void Start () {

		//Make reference to transform
		cachedTransform = transform;
 
		//Save starting position
		startingPos = cachedTransform.position;

	}
	

	// Update is called once per frame
	void Update () {
		
		if(dragEnabled)
		{
			if(Input.touchCount > 0)
			{
				Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
	 			Touch touch = Input.GetTouch(0);
				Ray ray = Camera.main.ScreenPointToRay (touch.position);
				
				//Switch through touch events
				switch(touch.phase)
				{
					case TouchPhase.Began:
						// Construct a ray from the current touch coordinates
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit, 300.0f)){
							if(hit.transform.gameObject.GetComponent<CR_Drag>() == this)
							{
								Instantiate (particle, transform.position, transform.rotation);
								startDrag = true;
							}
						}
					break;
					case TouchPhase.Moved:	
						if(startDrag) DragObject(deltaPosition);
					break;
					case TouchPhase.Ended:	
						startDrag = false;
					break;
	
				}
	 
			}
		}
	}

	

	/// <summary>
	/// Drags the object.
	/// </summary>
	/// <param name='deltaPosition'>
	/// Delta position.
	/// </param>

	void DragObject(Vector2 deltaPosition)
	{
		Vector3 cameraPosition = Camera.main.transform.position;
		// Mathf.Clamp(value, min, max) // clamps a value between a min and a max
		cachedTransform.position = new Vector3(Mathf.Clamp((deltaPosition.x * dragSpeed) + cachedTransform.position.x, 

			cameraPosition.x - horizontalLimit, cameraPosition.x + horizontalLimit), // min and max [x]

			Mathf.Clamp((deltaPosition.y * dragSpeed) + cachedTransform.position.y, 

			cameraPosition.y - verticalLimit, cameraPosition.y + verticalLimit), // min and max [y]

			cachedTransform.position.z);	

	}
	
	public void SnapToMe(CR_Snap snap)
	{
		if(snap == snapTarget || snapTarget == null)
		{
			// set position to snap position.
			transform.position = snap.transform.position;
			dragEnabled = false;
		}
	}

}