using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetect : MonoBehaviour
{
    public AudioSource Jump;
    public Rigidbody rigb;
    public bool CanJump;
    public float Jumping = 1225f;
    public GameObject Player;
    private NewBehaviourScript playerscript;
    
    void Start()
    {
        playerscript = Player.GetComponent<NewBehaviourScript>();
        CanJump = playerscript.JumpAbility; 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanJump == true)
        {
            rigb.AddForce(0, Jumping, 0* Time.deltaTime);
            Jump.Play();
            CanJump = false;
        }
    }
    
    void CantJump()
    {
        if(CanJump)
        {
            CanJump = false;
        }
    }
    
    void OnTriggerEnter()
    {
        CanJump = true;
    }

    void OnTriggerStay()
    {
        CanJump = true;
    }
    void OnTriggerExit()
    {
        Invoke("CantJump",0.2f);
        
    }
}
