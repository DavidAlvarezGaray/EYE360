using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 2.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    public GameObject luz_lateral_izquierda;
    public GameObject luz_lateral_derecha;
    public GameObject luz_central;

    // Renderer rend;

    public Material[] material;
    public int x;
    Renderer rend;


    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        // carLights = GetComponent<CarLights>();


        x = 0;
        // GetComponentsInChildren 
        rend = GetComponentInChildren<Renderer>();
        rend.enabled = true;
        luz_lateral_derecha.SetActive(false);
        luz_lateral_izquierda.SetActive(false);
        luz_central.SetActive(false);

    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();

    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if (control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical")*(-1);
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
            foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
        
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void BrakeLightsOff() {
        luz_lateral_derecha.SetActive(false);
        luz_lateral_izquierda.SetActive(false);
        luz_central.SetActive(false);
    }

    void BrakeLightsOn() {
        luz_lateral_derecha.SetActive(true);
        luz_lateral_izquierda.SetActive(true);
        luz_central.SetActive(true);
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            BrakeLightsOn();
        }
        else {
            BrakeLightsOff();
        }

        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }

            //  carLights.isBackLightOn = true;
            // carLights.OperateBackLights();
    
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

            // carLights.isBackLightOn = false;
            // carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            //var dirtParticleMainSettings = wheel.smokeParticle.main;

            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}