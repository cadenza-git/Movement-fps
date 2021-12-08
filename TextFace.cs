using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFace : MonoBehaviour
{
    
    public Transform Player; // Reference of the player

    
    void Update()
    {
        transform.LookAt(Player);
        transform.Rotate(0,180,0); // Otherwise the text looks the other way
    }
}
