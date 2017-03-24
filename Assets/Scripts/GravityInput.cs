using UnityEngine;

public class GravityInput : MonoBehaviour
{
    enum Sensors { None, Accelerometar, Gyroscope, Fusion }

    private Sensors activeSensors;

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

    void FixedUpdate()
    {
        switch (activeSensors)
        {
            case Sensors.Accelerometar:
                Physics.gravity = Input.acceleration;
                break;
            case Sensors.Gyroscope:
                Physics.gravity = Input.gyro.userAcceleration;
                break;
            case Sensors.Fusion:
                Physics.gravity = (Input.gyro.userAcceleration + Input.acceleration) / 2;
                break;
        }
    }
}
