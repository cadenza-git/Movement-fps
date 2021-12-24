using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource Feets;
    public AudioSource Fall;
    [Space]
    
    [Header("Movement")]
    public GameObject CheckSphere;
    private JumpDetect jumpDetect;
    public bool JumpAbility = true;
    public float SpeedLock;
    private float StrafeSpeedNeuter;
    public Rigidbody rb;
    public float strafe = 50f;
    public float Forward = 75f;
    public float Gravity = 1000f;
    public float CrouchDown = 30f;
    public float AirSpeedNeuter = 1;
    public float CrouchSpeed = 1f;
    public float MaxSpeed = 50f;
    public float CurrSpeed;
    public float StepHeight = 0.5f;
    private float WallStrafeNeuter = 1.0f;
    [Space]
   	
    [Header("Life/Damage")]
    public int life = 100;
    public Light light;
    private Color newColor = new Color(0.4179907f,0.2104842f,0.8113208f, 1f);
    private int FallDamage;
    private bool HasTurnedOff;
    public GameObject Enemy;
    
    
    public bool CheckWallRun()
    {
        RaycastHit hit;
        bool CanWallRun;
        if(Physics.Raycast( new Vector3(transform.position.x,transform.position.y-0.95f,transform.position.z ), transform.TransformDirection(Vector3.left), out hit, 1.5f) || Physics.Raycast( new Vector3(transform.position.x,transform.position.y-0.95f,transform.position.z ), transform.TransformDirection(Vector3.right), out hit, 1.5f) )
        {
            if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z ), transform.TransformDirection(Vector3.left), out hit, 1.5f) || Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z ), transform.TransformDirection(Vector3.right), out hit, 1.5f) )
            {
                CanWallRun = true;
                
                
            }
            else
            {
                CanWallRun = false;
            }
        }
        else
        {
            CanWallRun = false;
        	    
        }
        
        
        return CanWallRun;
        
    }
    
    public string CheckWallRunSide()
    {
        RaycastHit hit;
        string WallRunSide;
        if(Physics.Raycast( new Vector3(transform.position.x,transform.position.y+0.95f,transform.position.z ), transform.TransformDirection(Vector3.left), out hit, 1.5f) && Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.95f,transform.position.z ), transform.TransformDirection(Vector3.left), out hit, 1.5f) )
        {
            WallRunSide = "left";
            return WallRunSide;
        }
        if(Physics.Raycast( new Vector3(transform.position.x,transform.position.y+0.95f,transform.position.z ), transform.TransformDirection(Vector3.right), out hit, 1.5f) && Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.95f,transform.position.z ), transform.TransformDirection(Vector3.right), out hit, 1.5f) )
        {
            WallRunSide = "right";
            return WallRunSide;
        }
        else
        {
            WallRunSide = "null";
        }
        return WallRunSide;
    }
    
    
    bool CheckStep()
    {
        bool IsStep;
        RaycastHit hit;
        if(Physics.Raycast( new Vector3(transform.position.x,transform.position.y-0.95f,transform.position.z ), transform.TransformDirection(Vector3.forward), out hit, 0.97f))
        {
            if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y-0.95f+StepHeight,transform.position.z ), transform.TransformDirection(Vector3.forward), out hit, 0.97f))
            {
                IsStep = false;
                
            }
            else
            {
                IsStep = true;
            }
        }
        else
        {
            IsStep = false;
        }
        
        
        return IsStep;
        
        
    }
    
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
        Feets.Play();
        jumpDetect = CheckSphere.GetComponent<JumpDetect>();
        rb = GetComponent<Rigidbody>();
        light.color = newColor;
        
    }
    void Update()
    {
        JumpAbility = jumpDetect.CanJump;
        CurrSpeed = rb.velocity.magnitude;
        SpeedLock = (MaxSpeed - CurrSpeed)/MaxSpeed;
        SpeedLock = Mathf.Clamp(SpeedLock, 0.15f, 100);
        
        
        if(SceneManager.GetActiveScene().buildIndex == 7)
        {
            if(Input.GetKey(KeyCode.E))
            {
                Instantiate(Enemy, new Vector3(0,0,0), Quaternion.identity );
            }
        }
        
        Feets.volume = 1-SpeedLock;
        if(!JumpAbility)
        {
            Feets.volume = 0;
        }
        
        if(Input.GetKey(KeyCode.H))
        {
            life = 100;
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
            case int n when (n<=100):
                light.color = newColor;
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
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CrouchSpeed = 0.35f;
        }
        else
        {
            CrouchSpeed = 1f;
        }
    }
    void FixedUpdate()
    {
        if(CheckWallRun())
        {
            WallStrafeNeuter = 0.5f;
        }
        else
        {
            WallStrafeNeuter = 1.0f;
        }
        
        if(CheckStep() && (Input.GetKey(KeyCode.W))) //Step Controller
        {
            Debug.Log("Step");
            rb.AddForce(0,270,0);
            rb.AddRelativeForce(-Vector3.forward * (135*CrouchSpeed *AirSpeedNeuter));
            AirSpeedNeuter = 0.1f;
            
        }
        
        
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
            rb.AddRelativeForce(-strafe * WallStrafeNeuter * StrafeSpeedNeuter * SpeedLock * CrouchSpeed,0,0 * Time.deltaTime);
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
            rb.AddRelativeForce(strafe * WallStrafeNeuter * StrafeSpeedNeuter * SpeedLock * CrouchSpeed,0,0 * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddRelativeForce(0,-CrouchDown,0);
        }
        
        rb.AddForce(0,-Gravity,0);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
