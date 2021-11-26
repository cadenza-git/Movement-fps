//Handles input in relation to moving the Player around.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
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
    
    
    
    
    private void OnCollisionEnter(Collision collision)
    {
        //This form of testing to see if the player can jump is pretty terrible, may change in the future
        JumpAbility = true;
    }
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        CurrSpeed = rb.velocity.magnitude;
        SpeedLock = (MaxSpeed - CurrSpeed)/MaxSpeed; //Makes it so that if they player is travelling faster than
        //a specific limit, the effect they have in game is lessened, in a way that soft caps the player, instead of a
        //harsh limit. took me more time than average to derive so i guess im proud or somt idk
        
        if (Input.GetKey("escape"))
        {
            Application.Quit(); //closes game when built
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpAbility == true) //Done in update due to clashes with other inputs 
        {
            rb.AddForce(0, Jumping, 0* Time.deltaTime);
            JumpAbility = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))    //Slows player down when crouching
        {
            CrouchSpeed = 0.4f;
        }
        else
        {
            CrouchSpeed = 1f;
        }
    }
    
    void FixedUpdate()
    {
        
        if (JumpAbility != true)    //Player has less maneouverability in the air, but strafing is still relatively stron (source)
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
        if (Input.GetKey(KeyCode.LeftShift))    //Gives the player more maneouverability, as crouch also pushes down
        {
            rb.AddRelativeForce(0,-CrouchDown,0);
        }
        
        rb.AddForce(0,-Gravity,0);  //Player falls faster and jumping feels more realisitic
        
    }
}
