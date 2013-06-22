using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    // public variables:
    public float gasIncrement; // how much gas is gained when collecting a can

    // private variables:
    private Transform fuelMeter;
    private float startScale;

    void Start()
    {
        fuelMeter = transform.FindChild("Pack").transform.FindChild("fuelTransform");
        startScale = fuelMeter.localScale.y;
    }
	
	void Update() {
		if(Input.GetAxis("Fire3") > 0)
		{
			addFuel();
		}
	}

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Gas"))
        {
            addFuel();
			Destroy(hit.gameObject);
        }
    }
	
	private void addFuel()
	{
		if (fuelMeter.localScale.y + gasIncrement < startScale)
        {
            fuelMeter.localScale += Vector3.up * gasIncrement;
        }
        else
        {
            fuelMeter.localScale = new Vector3(fuelMeter.localScale.x, startScale, fuelMeter.localScale.z);
        }
	}
}
