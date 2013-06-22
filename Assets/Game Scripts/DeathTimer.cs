using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {
	
	public bool startImmediately = true;
	private bool countdownStarted = false;
	
	public float timeToDeath;
	
	// Use this for initialization
	void Start () {
		if(startImmediately)
		{
			Destroy(gameObject, timeToDeath);
			countdownStarted = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(startImmediately && countdownStarted != true)
		{
			countdownStarted = true;
			Destroy(gameObject, timeToDeath);
		}
	}
	
	public void startTheCountdown()
	{
		startImmediately = true;
	}
}
