using UnityEngine;
using System.Collections;
 
public class PARTICLEKILL: MonoBehaviour
{
    public ParticleSystem ps;
 
 
    public void Start() 
    {
         ps = GetComponent<ParticleSystem>();
    }
 
    public void Update() 
    {
        if(ps)
        {
            if(!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
