using UnityEngine;

public class GravityInput : MonoBehaviour
{
    public enum Sensors { None, Accelerometar, Gyroscope, Fusion }
    public Sensors activeSensors;
    public Vector3 gravity;
    public float gravityMagnitude = 5f;

    void Awake()
    {
        activeSensors = Sensors.None;

        if (SystemInfo.supportsAccelerometer)
        {
            activeSensors = Sensors.Accelerometar;
        }

        if (SystemInfo.supportsGyroscope)
        {
            activeSensors = Sensors.Gyroscope;
        }

        if (SystemInfo.supportsGyroscope && SystemInfo.supportsAccelerometer)
        {
            activeSensors = Sensors.Fusion;
        }
    }

    private Vector3 tempGravity;
    void FixedUpdate()
    {
        switch (activeSensors)
        {
            case Sensors.Accelerometar:
                tempGravity = Input.acceleration;
                break;
            case Sensors.Gyroscope:
                tempGravity = Input.gyro.userAcceleration;
                break;
            case Sensors.Fusion:
                tempGravity = (Input.gyro.userAcceleration + Input.acceleration) / 2;
                break;
        }

        gravity = tempGravity * gravityMagnitude;
        Physics.gravity = gravity;
    }
}
