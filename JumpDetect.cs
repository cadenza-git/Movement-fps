using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetect : MonoBehaviour
{
    public AudioSource Jump;
    public Rigidbody rigb;
    public bool CanJump;
    public float SpeedLock;
    public float Jumping = 1225f;
    public float WallRunY = 63;
    public float WallRunForward = 265;
    private float JumpAirModifier = 1.0f;
    private float JumpDirect = 1.0f;
    public GameObject Player;
    private NewBehaviourScript playerscript;
    
    void Start()
    {
        playerscript = Player.GetComponent<NewBehaviourScript>();
    }
    void Update()
    {
        SpeedLock = playerscript.SpeedLock; 
        
        if (Input.GetKeyDown(KeyCode.Space) && CanJump == true)
        {
            rigb.AddForce(0, Jumping *JumpAirModifier, 1*JumpDirect);
            JumpAirModifier = 1.0f;
            JumpDirect = 1.0f;
            Jump.Play();
            CanJump = false;
        }
    }
    
    void FixedUpdate()
    {
        if(playerscript.CheckWallRun() && Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) ) //Wallrun Controller
        {
            Debug.Log("Shat meself");
            rigb.AddRelativeForce(0,WallRunY,WallRunForward*SpeedLock);
            CanJump = true;
            JumpAirModifier = 0.05f;
            JumpDirect = 1.0f;
        }
        if(!playerscript.CheckWallRun())
        {
            JumpAirModifier = 1.8f;
            JumpDirect = 300.0f;
        }
    }
    
    void CantJump()
    {
        if(CanJump)
        {
            CanJump = false;
            JumpAirModifier = 1.0f;
            JumpDirect = 1.0f;
        }
    }
    
    void OnTriggerEnter()
    {
        JumpDirect = 1.0f;
        CanJump = true;
        JumpAirModifier = 1.0f;
        
    }

    void OnTriggerStay()
    {
        JumpDirect = 1.0f;
        CanJump = true;
        JumpAirModifier = 1.0f;
    }
    void OnTriggerExit()
    {
        Invoke("CantJump",0.2f);
        
    }
}
