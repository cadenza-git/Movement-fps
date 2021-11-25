using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //public Transform CameraRotation;
    //public Transform MainObject;
    //public Transform Camera;
    //public AudioSource FootSteps;
    public bool JumpAbility = false;
    public float strafe = 50f;
    public float Forward = 75f;
    public float Jumping = 1000f;
    public float Gravity = 1000f;
    public float CrouchDown = 30f;
    public float AirSpeedNeuter = 1;
    public float CrouchSpeed = 1f;
    public float MaxSpeed = 50f;
    public float CurrSpeed;
    private float SpeedLock;
    private float StrafeSpeedNeuter;
    public Rigidbody rb;
    
    //CharacterController characterController;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("I am extremely emasculate");
        JumpAbility = true;
    }
    // Start is called before the first frame updateS
    void Start()
    {
        //FootSteps = GetComponent<AudioSource>();
        //GetComponent.Source>().Play();
        //GetComponent<AudioSource>().Pause();
        rb = GetComponent<Rigidbody>();
        //CameraRotation = GetComponent<Transform>();
        //MainObject = GetComponent<Transform>();
        //characterController = GetComponent<CharacterController>();
        
    }
    void Update()
    {
        CurrSpeed = rb.velocity.magnitude;
        SpeedLock = (MaxSpeed - CurrSpeed)/MaxSpeed;
        //float SpeedLock = maxSpeed - speed;
        //SpeedLock = SpeedLock/maxSpeed;
        //if (JumpAbility == true)
        //{
            //GetComponent<AudioSource>().UnPause();
        //}
        //else
        //{
          //  GetComponent<AudioSource>().Pause();
        //}
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpAbility == true)
        {
            rb.AddForce(0, Jumping, 0* Time.deltaTime);
            JumpAbility = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CrouchSpeed = 0.4f;
        }
        else
        {
            CrouchSpeed = 1f;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //CameraRotation.rotation.y = MainObject.rotation.y;
        //MainObject.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
        //MainObject.transform.rotation = Quaternion.Euler(eulerRotation);
        
        if (JumpAbility != true)
        {
            AirSpeedNeuter = 0.25f;
            StrafeSpeedNeuter = 0.8f;
        }
        else
        {
            AirSpeedNeuter = 1f;
            StrafeSpeedNeuter = 1f;
        }
        
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(-strafe * StrafeSpeedNeuter * SpeedLock * CrouchSpeed,0,0 * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(0,0,-Forward * SpeedLock * AirSpeedNeuter * CrouchSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce( 0 , 0 , Forward * SpeedLock * AirSpeedNeuter * CrouchSpeed);
        }
        if (Input.GetKey(KeyCode.M))
        {
            rb.AddRelativeForce( 0 , 0 , 10000);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(strafe * StrafeSpeedNeuter * SpeedLock * CrouchSpeed,0,0 * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddRelativeForce(0,-CrouchDown,0);
        }
        
        rb.AddForce(0,-Gravity,0);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
