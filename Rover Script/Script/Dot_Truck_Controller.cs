using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Dot_Truck : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	public bool reverseTurn;
 
}
   
 


public class Dot_Truck_Controller : MonoBehaviour {

	public float maxMotorTorque; //sets the torque of the rover
	public float maxSteeringAngle; //sets the angle of the steering 
	public List<Dot_Truck> truck_Infos;
    public AudioManager manageAudio; //access from sound manager

    JointSpring carSpring;

    //front wheels
    public float gripfront;
    public float suspensionfront;
    public float springfront;

    //rear wheels
    public float griprear;
    public float suspensionrear;
    public float springrear;

    public void VisualizeWheel(Dot_Truck wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose ( out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
    }
    public void Awake()
    {
        
    }
    public void Start()
    {
        //Grip
        gripfront = PlayerPrefs.GetFloat("gripfront", 0);
        if (gripfront < 0.5f)
        {
            gripfront = 1f;
        }

        //Suspension
        suspensionfront = PlayerPrefs.GetFloat("suspensionfront", 0);
        if (suspensionfront < 0.1f)
        {
            suspensionfront = 0.3f;
        }

        //Spring
        springfront = PlayerPrefs.GetFloat("springfront", 0);
        if (springfront < 1000f)
        {
            springfront = 25500f;
        }
    }

    //public void Start()
    //{
    //manageAudio.roverSound.play ();
    //}

    //public void Awake()
    //{
    //manageAudio.roverSound.Play();
    //}

    public void Update()
	{
        
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));
        if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
        } else {
			brakeTorque = 0;
		}

        foreach (Dot_Truck truck_Info in truck_Infos)
		{
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((truck_Info.reverseTurn)?-1:1)*steering;
			}

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = motor;
				truck_Info.rightWheel.motorTorque = motor;
                
            }

			truck_Info.leftWheel.brakeTorque = brakeTorque;
			truck_Info.rightWheel.brakeTorque = brakeTorque;

			VisualizeWheel(truck_Info);
            manageAudio.roverSound.Play(); //audio play defined in update
        }

	}


}