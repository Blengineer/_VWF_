using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public string keyName;
	// observer system needed to notify others when a collectable is collected
	public IObserver[] observers;
	
	private bool collected = false; 
	
	void OnTriggerEnter(Collider c)
	{
		Debug.Log("passing through a collectable");
		c.transform.GetComponent<CartControl>().handleCollection(this);
	}
	
	public void setCollected()
	{
		collected = true;
		for(int i = 0; i < observers.Length; i++)
		{
			observers[i].GetComponent<IObserver>().simpleUpdate();
		}
	}
}
