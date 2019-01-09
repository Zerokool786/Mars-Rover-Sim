using UnityEngine;

public class CarCam : MonoBehaviour
{
    Transform carCam;
    Transform car;
    Rigidbody carPhysics;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 8.0f;

    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float cameraRotationSpeed = 5.0f;

    void Awake()
    {
        carCam = Camera.main.GetComponent<Transform>();
        car = GetComponent<Transform>();
        carPhysics = car.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        Quaternion look;

        //displaced car position (generates an output camera position separate from car's)
        Vector3 targetCarPosition = new Vector3(car.position.x, car.position.y + 3, car.position.z - 8);

        // Moves the camera to match the car's position.
        carCam.position = Vector3.Lerp(carCam.position, targetCarPosition, cameraStickiness * Time.fixedDeltaTime);

        // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
        if (carPhysics.velocity.magnitude < rotationThreshold)
            look = Quaternion.LookRotation(car.forward);
        else
            look = Quaternion.LookRotation(car.forward); //Quaternion.LookRotation(carPhysics.velocity.normalized);

        // Rotate the camera towards the velocity vector.
        look = Quaternion.Slerp(carCam.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
        carCam.rotation = look;
    }
}