using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float maxBreakingTorque;

    private float curMotorTorque; // the motor torque to apply to wheel in next fixed update
    private float curSteeringAngle;
    private float curBreakingTorque;

    public void Update()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        curMotorTorque = motor;
        curSteeringAngle = steering;
        if (Input.GetKey(KeyCode.Space))
            curBreakingTorque = maxBreakingTorque;
        else
            curBreakingTorque = 0;
    }

    public void FixedUpdate()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = curSteeringAngle;
                axleInfo.rightWheel.steerAngle = curSteeringAngle;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = curMotorTorque;
                axleInfo.rightWheel.motorTorque = curMotorTorque;

                axleInfo.leftWheel.brakeTorque = curBreakingTorque;
                axleInfo.rightWheel.brakeTorque = curBreakingTorque;
            }
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}