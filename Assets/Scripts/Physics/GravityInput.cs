using System.Collections.Generic;
using UnityEngine;

public class GravityInput : MonoBehaviour
{
    public enum Sensors { None, Accelerometar, Gyroscope, Fusion }

    [Header("Read Only")]
    public Sensors activeSensors;
    public Vector3 gravity;

    [Header("Gravity - Tweakable")]
    public float gravityMagnitude = 5f;
    public float randomizerValue = 1f;

    [Header("Low Pass Filter")]
    public bool lowPassFilterActive = true;
    public float accelerometerUpdateInterval = 1f / 60f;
    public float lowPassKernelWidthInSeconds = 1;
    private Vector3 lowPassValue = Vector3.zero;

    private Vector3 LowPassFilterAccelerometer(Vector3 inputValue)
    {
        float LowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = inputValue;
        lowPassValue = Vector3.Lerp(lowPassValue, inputValue, LowPassFilterFactor);
        return lowPassValue;
    }

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

        if (lowPassFilterActive)
        {
            LowPassFilterAccelerometer(tempGravity);
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
                Vector3 randomizedGravity;

                randomizedGravity.x = gravity.x + Random.Range(-randomizerValue, randomizerValue);
                randomizedGravity.y = gravity.y + Random.Range(-randomizerValue, randomizerValue);
                randomizedGravity.z = gravity.z + Random.Range(-randomizerValue, randomizerValue);

                rb.AddForce(randomizedGravity, ForceMode.Acceleration);
            }
        }
    }
}