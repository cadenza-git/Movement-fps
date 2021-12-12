using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDetect : MonoBehaviour
{
    public bool CanJump;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        CanJump = true;
    }

    void OnTriggerExit()
    {
        CanJump = false;
    }
}
