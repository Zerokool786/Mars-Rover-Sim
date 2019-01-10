using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour 
{
    //the wheel colliders
    public WheelCollider WheelFL; //front colliders
    public WheelCollider WheelFR; 
    public WheelCollider WheelML; //middle colliders
    public WheelCollider WheelMR; 
    public WheelCollider WheelBL; // back colliders
    public WheelCollider WheelBR;

    public GameObject FL;//the wheel gameobjects
    public GameObject FR; //front left mesh
    public GameObject ML; //middle left mesh
    public GameObject MR;  //middle right mesh
    public GameObject BL; //back left mesh
    public GameObject BR; // back right mesh


    public float topSpeed = 250f;//the top speed
    public float maxTorque = 200f;//the maximum torque to apply to wheels
    public float maxSteerAngle = 45f;
    public float currentSpeed;
    public float maxBrakeTorque = 2200;


    private float Forward;//forward axis
    private float Turn;//turn axis
    private float Brake;//brake axis

    public Rigidbody rb;//rigid body of car


    void Start () 
	{
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () //fixed update is more physics realistic
	{
        Forward = Input.GetAxis("Vertical");
        Turn = Input.GetAxis("Horizontal");
        Brake = Input.GetAxis("Jump");

        WheelFL.steerAngle = maxSteerAngle * Turn; //turns front wheel colliders only
        WheelFR.steerAngle = maxSteerAngle * Turn;

        currentSpeed = 2 * 22 / 7 * WheelBL.radius * WheelBL.rpm * 60 / 1000; //calculating speed in kmph

        if(currentSpeed < topSpeed)
        {

            WheelBL.motorTorque = maxTorque * Forward;//run the wheels on back left and back right
            WheelBR.motorTorque = maxTorque * Forward;
        }//the top speed will not be accurate but will try to slow the car before top speed

        WheelBL.brakeTorque = maxBrakeTorque * Brake;
        WheelBR.brakeTorque = maxBrakeTorque * Brake;
        WheelML.brakeTorque = maxBrakeTorque * Brake; //middle collider brakes
        WheelMR.brakeTorque = maxBrakeTorque * Brake; //middle collider brakes
        WheelFL.brakeTorque = maxBrakeTorque * Brake;
        WheelFR.brakeTorque = maxBrakeTorque * Brake;

    }
    void Update()//update is called once per frame
    {
        Quaternion flq;//rotation of wheel collider
        Vector3 flv;//position of wheel collider
        WheelFL.GetWorldPose(out flv,out flq);//get wheel collider position and rotation
        FL.transform.position = flv; //transform position and rotation of front left wheel mesh
        FL.transform.rotation = flq;

        Quaternion Blq;//rotation of wheel collider
        Vector3 Blv;//position of wheel collider
        WheelBL.GetWorldPose(out Blv, out Blq);//get wheel collider position and rotation
        BL.transform.position = Blv; // transform postion and rotation of back left wheel mesh
        BL.transform.rotation = Blq;

        Quaternion mlq; //rotation of middle left wheel collider
        Vector3 mlv; // position of middle left wheel collider
        WheelML.GetWorldPose(out mlv, out mlq); //get wheel collider position and rotation
        ML.transform.position = mlv;
        ML.transform.rotation = mlq;

        Quaternion mrq; //rotation of middle right wheel collider
        Vector3 mrv; //position of middle right wheel collider
        WheelMR.GetWorldPose(out mrv, out mrq); //get wheel collider position and rotation
        MR.transform.position = mrv;
        MR.transform.rotation = mrq;

        Quaternion fRq;//rotation of wheel collider
        Vector3 fRv;//position of wheel collider
        WheelFR.GetWorldPose(out fRv, out fRq);//get wheel collider position and rotation
        FR.transform.position = fRv;
        FR.transform.rotation = fRq;

        Quaternion BRq;//rotation of wheel collider
        Vector3 BRv;//position of wheel collider
        WheelBR.GetWorldPose(out BRv, out BRq);//get wheel collider position and rotation
        BR.transform.position = BRv;
        BR.transform.rotation = BRq;

    }
}
