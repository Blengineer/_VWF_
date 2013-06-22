using UnityEngine;
using System.Collections;

public class Butt : Hobo {
	
	public static float shotgunDamage = 10.0f;
	public Transform blastPlane;
	private Vector3 blastPos;
	
	// Use this for initialization
	void Start () {
		animName = "LiftAnim_butt";
	}
	
	// Update is called once per frame
	void Update () {
		toggleCheck = HUD.buttToggle;
		base.run();
	
		// if active...
		if(state == 1)
		{
			float blastShift = Mathf.Abs(Camera.main.transform.position.z)+blastPlane.transform.position.z;
    		Vector3 blastPos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,Input.mousePosition.y, blastShift));

			// allow the player to shoot stuff
			if ( Input.GetMouseButtonDown(0)) 
			{
				
				
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				//Transform target = GameObject.FindWithTag("select").transform; // use to register something as selected
				if (Physics.Raycast(ray, out hit, 300.0f)){
					//target.tag = "none";
					//hit.transform.tag = "select";
					if(hit.transform.gameObject.GetComponent<Damager>() != null)
					{
						hit.transform.gameObject.GetComponent<Damager>().dealDamage(shotgunDamage);
						Instantiate(Resources.Load("Shrapnel"), blastPos, new Quaternion());
					}
					
					else {
						Instantiate(Resources.Load("Shrapnel_2"), blastPos, new Quaternion());
					}
				}
				
				else {
					Instantiate(Resources.Load("Shrapnel_2"), blastPos, new Quaternion());
				}
			}
			
			
		}
	}
	
	void OnDrawGizmos () 
	{
		if(state == 1)
		{
    		Gizmos.color = Color.yellow;
   	 		Gizmos.DrawSphere (blastPos, 5.0f);
		}
	}
}
