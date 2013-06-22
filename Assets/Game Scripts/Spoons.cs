using UnityEngine;
using System.Collections;

public class Spoons : Hobo {

	// Use this for initialization
	void Start () {
		animName = "LiftAnim_spoons";
	}
	
	// Update is called once per frame
	void Update () {
		toggleCheck = HUD.spoonsToggle;
		base.run();
	}
}
