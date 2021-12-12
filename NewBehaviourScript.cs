using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject CheckSphere;
    private JumpDetect TellJump;
    public AudioSource Feets;
    public AudioSource Fall;
    public AudioSource Jump;
    public bool JumpAbility = true;
    public float strafe = 50f;
    public float Forward = 75f;
    public float Jumping = 1000f;
    public float Gravity = 1000f;
    public float CrouchDown = 30f;
    public float AirSpeedNeuter = 1;
    public float CrouchSpeed = 1f;
    public float MaxSpeed = 50f;
    public float CurrSpeed;
    public int life = 100;
    public Light light;
    private Color newColor = new Color(0.4179907f,0.2104842f,0.8113208f, 1f);
    private float SpeedLock;
    private float StrafeSpeedNeuter;
    public Rigidbody rb;
    private int FallDamage;
    private bool HasTurnedOff;
    
    //CharacterController characterController;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.relativeVelocity.y > 32.5 )
        {
            FallDamage = (int)(2*(collision.relativeVelocity.y-33f));
            Fall.Play();
            life = life-FallDamage;
        }
    }
    
    void Start()
    {
        TellJump = CheckSphere.GetComponent<JumpDetect>(); //references the current collision status
        Feets.Play();
        
        rb = GetComponent<Rigidbody>();
        light.color = newColor;
        
    }
    void Update()
    {
        JumpAbility = TellJump.CanJump;
        
        //t += 1f * Time.deltaTime;
        CurrSpeed = rb.velocity.magnitude;
        SpeedLock = (MaxSpeed - CurrSpeed)/MaxSpeed;
        SpeedLock = Mathf.Clamp(SpeedLock, 0, 100);
        Feets.volume = 1-SpeedLock;
        if(!JumpAbility)
        {
            Feets.volume = 0;
        }
        
        
        
        switch(life)
        {
            case int n when (n <= 20):
                light.color= Color.red;
                break;
            case int n when (n <= 40 && n <= 21):
                light.color= Color.cyan;
                break;
            case int n when (n <= 60 && n <= 41):
                light.color= Color.blue;
                break;
            case int n when (n <= 80 && n <= 61):
                light.color= Color.magenta;
                break;
        }
            
            
        if(life<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space) && JumpAbility == true)
        {
            rb.AddForce(0, Jumping, 0* Time.deltaTime);
            Jump.Play();
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
