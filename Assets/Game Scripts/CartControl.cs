using UnityEngine;
using System.Collections;

public class CartControl : MonoBehaviour {
	
	public float baseSpeed = 1000.0f;
	public float baseHP = 10.0f;
	public float currentHP;
	private Smokie p_smoke;
	private Spoons p_spoon;
	private Butt p_butt;
	
	// Use this for initialization
	void Start () {
		reset();
		// load the hobos:
		p_smoke = transform.FindChild("Smokie").GetComponent<Smokie>();
		p_spoon = transform.FindChild("Spoons").GetComponent<Spoons>();
		p_butt = transform.FindChild("Butt").GetComponent<Butt>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// main play state
		if(HUD.GameState == 0)
		{
			if(p_smoke.isActive())
			{
				rigidbody.AddForce(p_smoke.getFlameVector()*baseSpeed);
			}
			
			Debug.DrawRay(transform.position, transform.forward*3.5f, Color.yellow);
			
    		if (Physics.Raycast (transform.position, transform.forward, 3.5f)) {
				Debug.Log("Something above me!");
				
				dealDamage(5000.0f);
				
			}
			
			/*if (gameObject.rigidbody.IsSleeping())
			{
				Debug.Log("Sleeeeping");	
			}*/
		}
		
		// if the game is in "game over state"
		else if(HUD.GameState == 1)
		{
			if ( Input.GetMouseButtonDown(0)) 
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				if (Physics.Raycast(ray, out hit, 300.0f)){
					if(hit.transform.gameObject == this.gameObject)
					{
						animation.Play();
						currentHP = baseHP;
						HUD.GameState = 0;
						Camera.main.GetComponent<CameraFollow>().zoomOut();
					}
				}
			}
		}
	}
	
	// if you fail to break a box before it reaches the cart, the box will cause damage to the cart
	public void dealDamage(float quantity)
	{
		currentHP -= quantity;
		if(currentHP <= 0)
		{
			Debug.Log("You flipped it!");
			HUD.GameState = 1;
			Camera.main.GetComponent<CameraFollow>().zoomIn();
		}
	}
	
	// when passing by an item, spoons will try to grab it if he is ready
	public void handleCollection(Collectable item)
	{
		if(p_spoon.isActive())
		{
			item.setCollected();
			Destroy(item.gameObject);
		}
	}
	
	public void reset()
	{
		currentHP = baseHP;
	}
	
	
}
