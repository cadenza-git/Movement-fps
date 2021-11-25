//Attaches to Particle system which is instantiated, kills it after end of play,, so that scene doesn't become laggy

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
            //checks to see if the Particle is currently playing
            {
            
                Destroy(gameObject);
            }
        }
    }
}
