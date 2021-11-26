//Loads the next scene, currently set to do so when game object it is attached to is collided with, although LoadNextLevel
//can be used for UI etc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelReset : MonoBehaviour
{
    
    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void OnCollisionEnter()
    {
        ReLoad(); 
    }
    
}
