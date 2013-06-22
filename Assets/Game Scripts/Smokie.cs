using UnityEngine;
using System.Collections;

public class Smokie : Hobo {
	
	public Camera rayCamera; 
	public Transform gun;
	private float speedRot = 1.0f;
	private Vector3 flameVector = new Vector3(0,0,0);
	public float flameBoost = 1.0F; // how much force should be applied to the player from the energy of the flamethrower
	
	// Use this for initialization
	void Start () {
		animName = "LiftAnim_smokie";
	}
	
	// Update is called once per frame
	void Update () {
		toggleCheck = HUD.smokieToggle;
		base.run();
		
		if(state == 0)
		{
			// hide the gun
			gun.transform.GetChild(0).gameObject.renderer.enabled = false;
			gun.transform.GetChild(1).particleSystem.enableEmission = false;
		}
		
		if(state == 1)
		{
			// show the gun:
			gun.transform.GetChild(0).gameObject.renderer.enabled = true;
			gun.transform.GetChild(1).particleSystem.enableEmission = true;
			
			// set gun's position to follow the hand
			gun.transform.position = transform.GetChild(0).position;
			
			// Compute flame nozzle angle based on mouse position:
			//Ray ray = rayCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			Ray ray = rayCamera.ScreenPointToRay(new Vector3(0, Screen.height/2, 0));
			Debug.DrawRay(ray.origin, ray.direction * 75, Color.yellow);
			Debug.DrawLine(ray.GetPoint(5).normalized*10, ray.GetPoint(5).normalized*-10, Color.red);
			gun.LookAt(ray.GetPoint(75), Vector3.forward);
			
			// Now calculate the flameVector based on the nozzle angle:
			Vector3 goalVector = Vector3.Scale(gun.forward, new Vector3(1, 1, 0));
			goalVector.Normalize();
			goalVector *= -flameBoost; // invert the direction and apply an amount to the normalized vector
	        flameVector = Vector3.Lerp(flameVector, goalVector, Time.deltaTime * 5);
			//flameVector = flameVector * Input.GetAxis ("Fire1");
		}
	}
	
	public Vector3 getFlameVector()
	{
		return flameVector;
	}
}
