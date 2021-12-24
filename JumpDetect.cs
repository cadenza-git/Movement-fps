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
    public float WallRunY = 59.5f;
    public float WallRunForward = 170f;
    private float JumpAirModifier = 1.0f;
    private float JumpDirect = 1.0f;
    public bool TellCameraLean = false;
    public GameObject Player;
    private NewBehaviourScript playerscript;
    public string SideOfWallRun;
    public bool CanWallJump;
    
    
    void Start()
    {
        playerscript = Player.GetComponent<NewBehaviourScript>();
    }
    void Update()
    {
        SpeedLock = playerscript.SpeedLock; 
        
        if (Input.GetKeyDown(KeyCode.Space) && CanJump == true)
        {
            rigb.AddRelativeForce(0, Jumping *JumpAirModifier, 1*JumpDirect);
            JumpAirModifier = 1.0f;
            JumpDirect = 1.0f;
            Jump.Play();
            CanJump = false;
            JumpDirect = 1.0f;
        }
    }
    
    void FixedUpdate()
    {
        if(playerscript.CheckWallRun() && Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) ) //Wallrun Controller, when your on the wall
        {
            Debug.Log("Shat meself");
            rigb.AddRelativeForce(0,WallRunY,WallRunForward*(SpeedLock+0.1f));
            CanJump = false;
            SideOfWallRun = playerscript.CheckWallRunSide();
            TellCameraLean = true;
            JumpAirModifier = 1.6f;
            JumpDirect = 300.0f;
            CanWallJump = true;
            
        }
        if(!playerscript.CheckWallRun()) //when you're not wallrunning, or haven't touched the ground yet
        {
            TellCameraLean = false;
            JumpAirModifier = 1.85f;
            JumpDirect = 300.0f;
            
        }
        float JumpWallDirection = 1;
        switch (SideOfWallRun)
        {
            case "left":
                JumpWallDirection = 550;
                break;
            case "right":
                JumpWallDirection = -550;
                break;
        }
        if(playerscript.CheckWallRun() && Input.GetKey(KeyCode.Space) && CanWallJump)
        {
            rigb.AddRelativeForce(JumpWallDirection,374,0);
            CanWallJump = false;
        }
    }
    
    void CantJump()
    {
        if(CanJump)
        {
            CanJump = false;
            JumpAirModifier = 1.85f;
            JumpDirect = 300.0f;
        }
    }
    
    void OnTriggerEnter() //When you hit the ground
    {
        JumpDirect = 1.0f;
        CanJump = true;
        JumpAirModifier = 1.0f;
        
    }

    void OnTriggerStay() //while you're on the ground
    {
        JumpDirect = 1.0f;
        CanJump = true;
        JumpAirModifier = 1.0f;
    }
    void OnTriggerExit() //when you leave the ground
    {
        Invoke("CantJump",0.1f);
        
    }
}
