using UnityEngine;
using System.Collections;

public class PlayerCar : MonoBehaviour {
	
	// These variables allow the script to power the wheels of the car.
	public WheelCollider FrontLeftWheel;
	public WheelCollider FrontRightWheel;
	
	// These variables are for the gears, the array is the list of ratios. The script
	// uses the defined gear ratios to determine how much torque to apply to the wheels.
	public float[] GearRatio;
	public int CurrentGear = 0;
	
	// These variables are just for applying torque to the wheels and shifting gears.
	// using the defined Max and Min Engine RPM, the script can determine what gear the
	// car needs to be in.
	public float EngineTorque = 230.0f;
	public float MaxEngineRPM = 3000.0f;
	public float MinEngineRPM = 1000.0f;
	private float EngineRPM = 0.0f;
	
	public float centerOfGravity_X = 0;
	public float centerOfGravity_Y = -1;
	public float centerOfGravity_Z = 0.25f;
	
	// Use this for initialization
	void Start () {
		rigidbody.centerOfMass += new Vector3(centerOfGravity_Y, centerOfGravity_X, centerOfGravity_Z);
	}
	
	// Update is called once per frame
	void Update () {
	
		// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
		// up to twice it's pitch, where it will suddenly drop when it switches gears.
		audio.pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0f ;
		// this line is just to ensure that the pitch does not reach a value higher than is desired.
		if ( audio.pitch > 2.0f ) {
			audio.pitch = 2.0f;
		}
	
		// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
		// multiplied by the user input variable.
		
		if(HUD.GameState == 0)
		{
			FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * 1;  //!!!
			FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * 1; //!!!
		}
		
		else {
			FrontLeftWheel.motorTorque = 0;
			FrontRightWheel.motorTorque = 0;
		}
	}

 	public void ShiftGears() {
		// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
		// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
		int AppropriateGear = CurrentGear;
		
		if ( EngineRPM >= MaxEngineRPM ) {
			
			for ( int i = 0; i < GearRatio.Length; i ++ ) {
				if ( FrontLeftWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
					AppropriateGear = i;
					break;
				}
			}
			
			CurrentGear = AppropriateGear;
		}
	
		if ( EngineRPM <= MinEngineRPM ) {
			AppropriateGear = CurrentGear;
			
			for ( int j = GearRatio.Length-1; j >= 0; j -- ) {
				if ( FrontLeftWheel.rpm * GearRatio[j] > MinEngineRPM ) {
					AppropriateGear = j;
					break;
				}
			}
			
			CurrentGear = AppropriateGear;
		}
	}

	void OnDrawGizmos () 
	{
		Gizmos.color = Color.magenta;
	 	Gizmos.DrawSphere (transform.position + rigidbody.centerOfMass, 1.0f);
	}
	
}
