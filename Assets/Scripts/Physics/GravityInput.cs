using System.Collections.Generic;
using UnityEngine;

public class GravityInput : MonoBehaviour
{
    public enum Sensors { None, Accelerometar, Gyroscope, Fusion }
    public Sensors activeSensors;
    public Vector3 gravity;
    public float gravityMagnitude = 5f;
    public float randRange = 0.1f;

    public List<Rigidbody> gravityBodies;

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

        gravityBodies = new List<Rigidbody>();
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

        // For testings (in editor)
        if (activeSensors != Sensors.None)
        {
            gravity = tempGravity * gravityMagnitude;
        }

        foreach (Rigidbody rb in gravityBodies)
        {
            if (rb != null)
            {
                // TODO - randomize gravity
                Vector3 randomizedGravity = gravity;
                rb.AddForce(randomizedGravity, ForceMode.Acceleration);
            }
        }
    }
}