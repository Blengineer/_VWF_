using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {
	
	// Required Objects:
	public FlameTrigger flameBlock;
	
	// Tweaks:
	public float blockSpawnRate = 0.3F; // time in seconds before spawning another hit box
	public float MAX_TEMPERATURE = 3; // time in seconds before flamethrower overheats
	public float lightBrightness; // the brightness of the light emitted by the flame thrower
    public float lightFadeSpeed; // how fast does this light fade away when the player stops spraying
    public float MAX_COOLDOWN = 2;	// time in seconds for flamethrower to cool-down after overheating
	public float depletionRate = 1; // how much fuel is taken away per logic tick
    public float flameBoost = 0.5F; // how much force should be applied to the player from the energy of the flamethrower
	
	// Counters: (all start at zero)
	private float currentSpawnCounter;
	private float currentTemperature;
	private float currentOverheat;
	
	// print-outs: (private variables that we will keep public so that their state is displayed)
	public Vector3 flameVector = new Vector3(0, 0, 0); // the direction of force applied by the flamethrower
	public bool overheated = false; // whether or not the flamethrower is overheated
	
	// Flame Emission stuff:
	private ParticleEmitter innerEmitter;
    private ParticleEmitter outerEmitter;
	private ParticleSystem newTestEmitter;
    private Light nearLight;
    private Light farLight;
	//private Vector3 flameVector = new Vector3(0, 0, 0);
	
	// Fuel Tank stuff:
    private Transform fuelMeter;
    private Material fuelTankMat;
	
	// Use this for initialization
	void Start () 
	{
		innerEmitter = transform.FindChild("Particle System_inner").gameObject.particleEmitter;
        outerEmitter = transform.FindChild("Particle System_outer").gameObject.particleEmitter;
		newTestEmitter = transform.FindChild("NewTestParticles").particleSystem;
        nearLight = transform.FindChild("nearLight").gameObject.light;
        farLight = transform.FindChild("farLight").gameObject.light;
		
		fuelMeter = gameObject.transform.parent.FindChild("Pack").transform.FindChild("fuelTransform");
        fuelTankMat = gameObject.transform.parent.FindChild("Pack").renderer.material;
		currentSpawnCounter = blockSpawnRate; // spawn 1 block right away when firing starts
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Compute flame nozzle angle based on mouse position:
		Ray ray = Camera.main.camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
		Debug.DrawRay(ray.origin, ray.direction * 5, Color.yellow);
		Debug.DrawLine(ray.GetPoint(5).normalized*2, ray.GetPoint(5).normalized*-2, Color.red);
		transform.LookAt(ray.GetPoint(5), Vector3.forward);
		
		// set the color of the backpack:
        currentTemperature += Time.deltaTime;
		Color tmpColor = fuelTankMat.color;
		tmpColor.g = 1 - (currentTemperature / MAX_TEMPERATURE);
		fuelTankMat.color = tmpColor;
		
		// handle turning the gun on or off:
		if (currentTemperature < MAX_TEMPERATURE && !overheated)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                flameOn();
            }
            else
            {
                flameOff();
                currentTemperature = 0;
            }
        }
		
        else
        {
            overheated = true;
            flameOff();
        }

        if (overheated)
        {
            currentOverheat += Time.deltaTime;
            if (currentOverheat > MAX_COOLDOWN)
            {
                overheated = false;
                currentTemperature = 0;
                currentOverheat = 0;
            }
        }
	}
	
	void flameOn()
    {
        if (fuelMeter.localScale.y > 0)
        {
            innerEmitter.emit = true;
            outerEmitter.emit = true;
			newTestEmitter.enableEmission = true;
            // interpolate light brightness:
            nearLight.intensity = Mathf.Lerp(nearLight.intensity, lightBrightness + (Random.value * 2), Time.deltaTime * lightFadeSpeed);
            farLight.intensity = Mathf.Lerp(farLight.intensity, (lightBrightness + (Random.value * 2)) / 3, Time.deltaTime * lightFadeSpeed);
            // decrease fuel:
            fuelMeter.localScale -= Vector3.up * Time.deltaTime * depletionRate;
			
			// Now calculate the flameVector based on the nozzle angle:
			Vector3 goalVector = Vector3.Scale(transform.forward, new Vector3(1, 1, 0));
			goalVector.Normalize();
			goalVector *= -flameBoost; // invert the direction and apply an amount to the normalized vector
			Debug.Log(transform.rotation.eulerAngles + " | " + goalVector);
            flameVector = Vector3.Lerp(flameVector, goalVector, Time.deltaTime * 5);
			
			// handle adding new hit blocks so that the flamethrower can actually hit stuff:
			currentSpawnCounter += Time.deltaTime;
			if(currentSpawnCounter > blockSpawnRate)
			{
				//Vector3 relativePos = target.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation(flameVector);
				//if (flameVector.x < 0) {
				//	Vector3 flipped = flameVector * -1;
				//	rotation = Quaternion.LookRotation(flipped);
				//}
				Object newThing = Instantiate(flameBlock, transform.position, rotation);
				FlameTrigger newtriggerBlock = (FlameTrigger)newThing;
				if (flameVector.x < 0) {
					newtriggerBlock.setDirection(-1);
				}
				currentSpawnCounter = 0;
			}
        }
    }

    void flameOff()
    {
        innerEmitter.emit = false;
        outerEmitter.emit = false;
		newTestEmitter.enableEmission = false;
        // interpolate light brightness:
        nearLight.intensity = Mathf.Lerp(nearLight.intensity, 0, Time.deltaTime * lightFadeSpeed);
        farLight.intensity = Mathf.Lerp(farLight.intensity, 0, Time.deltaTime * lightFadeSpeed);
        flameVector = Vector3.Lerp(flameVector, new Vector3(0, 0, 0), Time.deltaTime*13);
		currentSpawnCounter = blockSpawnRate;
    }
	
	public Vector3 getFlameVector()
	{
		return flameVector;
	}
}
