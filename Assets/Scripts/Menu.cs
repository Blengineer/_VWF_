using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private int levelGridIndex = -1; // the index in the array referring to what race to use for last names
	
	public string[] levelNames;
	public int startY = 30;
	public int margin = 20;
	
	// Use this for initialization
	void Start () {
		levelGridIndex = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		// for however many levels there are, create an array of buttons to allow the user to select a level.
		GUI.Label (new Rect (margin, margin+startY, 100, 30), "Select A Level:");
		
		int columns = 2;
		levelGridIndex = GUI.SelectionGrid (new Rect (margin, 50+30, Screen.width - (margin*4), 40), levelGridIndex, levelNames, columns);
	
		if(levelGridIndex > -1)
		{
			Application.LoadLevel(levelGridIndex+1);
		}
	}
}
