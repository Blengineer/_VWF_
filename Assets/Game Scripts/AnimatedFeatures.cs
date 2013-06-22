using UnityEngine;
using System.Collections;

public class AnimatedFeatures : IObserver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Implementation of IObserver<T> interface 
	public override void simpleUpdate() {
		animation.Play();
	}
}
