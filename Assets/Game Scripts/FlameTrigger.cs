using UnityEngine;
using System.Collections;

public class FlameTrigger : MonoBehaviour {
	
	public float scaleRate = 0.1F;
	public float speed = 1.0F;
	public float lifetime = 3.0F; // time in seconds before this block will disappear
	
	// counters:
	private float lifeCount;
	
	// printouts:
	public int direction = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// make it bigger over time:
		Vector3 scale = transform.localScale;
		scale.x += scaleRate;
		scale.y += scaleRate;
		scale.z += scaleRate;
		transform.localScale = scale;
		
		// move it forward:
		transform.Translate(transform.right * speed * Time.deltaTime * direction);
		
		// increment life:
		lifeCount += Time.deltaTime;
		if(lifeCount > lifetime) // kill self if lived full life
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("collided with something!");
		if(other.gameObject.CompareTag("target"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject); // destroy this trigger block
		}
	}
	
	// allow the flamecontroller to manage the direction that this will go 
	// (either forward or back depending on firing angle)
	public void setDirection(int dir)
	{
		direction = dir;
	}
}
