using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Android;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public float maxBreakingTorque;

    private float curMotorTorque; // the motor torque to apply to wheel in next fixed update
    private float curSteeringAngle;
    private float curBreakingTorque;

    [SerializeField] private Material backlight;
    private Color backLightBaseColor;
    private float baseBackLightEmission = 0.5f;
    private float fullBackLightEmission = 6f;

    private void Start()
    {
        backLightBaseColor = backlight.color;
        ChangeBackLight(baseBackLightEmission);
    }

    public void Update()
    {
        //HandleKeyBoardInputs();
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

    private void HandleKeyBoardInputs()
    {
        Accelerate(Input.GetAxis("Vertical"));
        Steer(Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.Space))
            PressBrake();
        else
            ReleaseBrake();
    }

    //for continious inputs ***********************************************
    public void Brake(float factor)
    { curBreakingTorque = maxBreakingTorque * factor; }

    public void Accelerate(float factor)
    { curMotorTorque = maxMotorTorque * factor; }

    public void Steer(float factor) 
    { curSteeringAngle = maxSteeringAngle * factor; }
    public void SteerRight(float factor)
    { //right steering should always have positive angle
        Steer(Mathf.Abs(factor));
    }
    public void SteerLeft(float factor)
    { //left steering should always have negative angle
        Steer(-Mathf.Abs(factor));
    }
    //*********************************************************************


    //for event based inputs***********************************************
    public void PressBrake()
    { 
        Brake(1f);
        ChangeBackLight(fullBackLightEmission);
    }
    public void ReleaseBrake()
    { 
        Brake(0f);
        ChangeBackLight(baseBackLightEmission);
    }

    public void PressAccelerate()
    { Accelerate(1f); }
    public void ReleaseAccelerate()
    { Accelerate(0f); }

    public void SteerToLeft()
    { SteerLeft(1f); }
    public void SteerToRight()
    { SteerRight(1f); }
    public void ReleaseSteering()
    { SteerRight(0f); }
    //*********************************************************************

    private void ChangeBackLight(float emission)
    {
        if (backlight == null)
            return;
        Color finalColor = backLightBaseColor * emission;
        backlight.SetColor("_EmissionColor", finalColor);
    }

    public void Reset()
    {
        ReleaseAccelerate();
        ReleaseSteering();
        ReleaseBrake();
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