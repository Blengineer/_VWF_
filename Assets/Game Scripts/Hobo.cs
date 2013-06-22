using UnityEngine;
using System.Collections;

public class Hobo : MonoBehaviour {
	
	protected int state = 0;
	protected string animName = "tmp";
	protected bool toggleCheck;
	
	public void run() {
		if(toggleCheck && state == 0)
		{
			this.animation[animName].speed = 1;
			this.animation.Play();
			state = 1;
		}
		else if(toggleCheck == false)
		{
			this.animation[animName].speed = -1;
			this.animation.Play();
			state = 0;
		}
	}
	
	public bool isActive() {
		return toggleCheck;
	}
}
