using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public float startTime = 5.0f;
	private float currentTime;
	
	public float flameDelay = 0.2f;
	private float delayCounter;
	private float startCount;
	
	public static int GameState = 0;
	// what character is active:
	public static bool spoonsToggle = false;
	public static bool smokieToggle = false;
	public static bool buttToggle = false;
	
	public Texture smokieImage;
	public Texture buttImage;
	public Texture spoonsImage;
	
	public GUITexture smokieTexture;
	public GUITexture buttTexture;
	public GUITexture spoonsTexture;
	
	// Use this for initialization
	void Start () {
		currentTime = startTime/10;
	}
	
	// Update is called once per frame
	void Update () {
		// count down to zero!
		currentTime -= Time.deltaTime/10;
		// TODO: 
		// slowly shrink a ring approaching the cart as timer approaches zero
	}
	
	void OnGUI () {
		
		if(GUI.Button(new Rect(Screen.width-110, 10, 100, 40), "Restart")) {
			Application.LoadLevel(Application.loadedLevel);
			HUD.GameState = 0;
		}
		
		// at the start of the game
		if(HUD.GameState == 0)
		{
			/*if(currentTime > 0.001)
			{
				GUI.Label(new Rect(Screen.width/2,Screen.height/2,100,100), currentTime+"");
				
				if(currentTime*10 <= 1)
				{
					GUI.Label(new Rect(Screen.width/2 - 30,Screen.height/2,100,100), "1");
				}
				else if (currentTime*10 <= 2)
				{
					GUI.Label(new Rect(Screen.width/2 - 30,Screen.height/2,100,100), "2");
				}
				else if (currentTime*10 <= 3)
				{
					GUI.Label(new Rect(Screen.width/2 - 30,Screen.height/2,100,100), "3");
				}
			}
			
			else {
				GUI.Label(new Rect(Screen.width/2 - 10,Screen.height/2,100,100), "GO!");
			}*/
			
			int bV = 6;
			
			Rect inset1 = new Rect(0, 0, Screen.width/3, Screen.height/bV);
			Rect inset2 = new Rect(Screen.width/3, 0, Screen.width/3, Screen.height/bV);
			Rect inset3 = new Rect(Screen.width - Screen.width/3, 0, Screen.width/3, Screen.height/bV);
			
			
			// For the next prototype I'm going to need to do a lot of modifications.
			// Whenever you press and hold on the screen it should activate the flamethrower.
			// If you tap on the screen it should fire a shotgun blast at that position (click and release)
			// If you drag on the screen you should be able to activate switches or grab things.
			
			// For now: 
			//	the first tap should both fire a shotgun blast and start triggering the flamethrower
			//  simply tapping on green things should activate them at this point.
			
			if(Input.touchCount > 0)
			{
				Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
	 			//Touch touch = Input.GetTouch(0);
				// Ray ray = Camera.main.ScreenPointToRay (touch.position);
				
				//The first touch will trigger flamethrower
				/*switch(touch.phase)
				{
					case TouchPhase.Began:
						smokieToggle = true;
					break;
					case TouchPhase.Ended:
						smokieToggle = false;
					break;
				}*/
				
				Touch touch;
				for(int i = 0; i < Input.touchCount; i++)
				{
					touch = Input.GetTouch(i);
					switch(touch.phase)
					{
						case TouchPhase.Began:
							smokieToggle = true;
							buttToggle = true;
							// Construct a ray from the current touch coordinates
							/*RaycastHit hit;
							if (Physics.Raycast(ray, out hit, 300.0f)){
								if(hit.transform.gameObject.GetComponent<CR_Drag>() == this)
								{
									Instantiate (particle, transform.position, transform.rotation);
									startDrag = true;
								}
							}*/
						break;
						case TouchPhase.Moved:	
							//if(startDrag) DragObject(deltaPosition);
						break;
						case TouchPhase.Ended:
							smokieToggle = false;
							buttToggle = false;
							//startDrag = false;
						break;
					}
				}
				
				delayCounter += Time.deltaTime;
			}
			
			
			/*spoonsTexture.pixelInset = inset2;
			smokieTexture.pixelInset = inset1;
			buttTexture.pixelInset = inset3;
			
			// Display Buttons:
			//spoonsToggle = GUI.Toggle(new Rect(0, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), spoonsToggle, spoonsImage);
			smokieToggle = GUI.Toggle(new Rect(0, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), smokieToggle, "Smokie");
			if(smokieToggle) {
				buttToggle = false;
				spoonsToggle = false;
			}
			//smokieToggle = GUI.Toggle(new Rect(Screen.width/3, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), smokieToggle, smokieImage);
			spoonsToggle = GUI.Toggle(new Rect(Screen.width/3, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), spoonsToggle, "Spooons");
			if(spoonsToggle) {
				smokieToggle = false;
				buttToggle = false;
			}
			//buttToggle = GUI.Toggle(new Rect(Screen.width - Screen.width/3, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), buttToggle, buttImage);
			buttToggle = GUI.Toggle(new Rect(Screen.width - Screen.width/3, Screen.height - Screen.height/bV, Screen.width/3, Screen.height/bV), buttToggle, "Butt");
			if(buttToggle) {
				spoonsToggle = false;
				smokieToggle = false;
			}*/
		}
		
		else if(HUD.GameState == 1)
		{
			GUI.Label(new Rect(Screen.width/2,Screen.height/2,100,100), "Touch It to Flip It!");
			deactivateToggles();
		}
	}
	
	public void deactivateToggles()
	{
		buttToggle = false;
		smokieToggle = false;
		spoonsToggle = false;
	}
}
