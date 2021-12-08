using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFace : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        transform.Rotate(0,180,0);
    }
}
