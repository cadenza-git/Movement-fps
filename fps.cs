//Handles HUD, tells player FPS, Speed and whether they can shoot an explosive or not

using UnityEngine;
using UnityEngine.UI;
 
public class fps : MonoBehaviour
{
    //public MeshRenderer EnableShootTell;
    public GameObject FPSCAM;
    private MouseLook Mlook;
    public int avgFrameRate;
    public float ShowSpeed;
    public Text ShootTell;
    public Text display_Text;
    public Text SpeedText;
    public Rigidbody player;
    private string TellMePls;
    
    public void Start ()
    {
        Mlook = FPSCAM.GetComponent<MouseLook>(); //References canShoot from MouseLook for use.
    }
    public void Update ()
    {
        
        float current = 0;
        float ShowSpeed = player.velocity.magnitude; //Gets Speed
        current = (int)(1f / Time.unscaledDeltaTime); //Gets FPS
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS ";
        SpeedText.text = ShowSpeed.ToString();
        if (!Mlook.canShoot)
        {
            //EnableShootTell.enabled = false;(usable if using an icon)
            TellMePls = "No";
        }
        else
        {
            //EnableShootTell.enabled = true;(usable if using an icon)
            TellMePls = "Yes";
        }
        ShootTell.text = TellMePls; //Displays the current status of TellMePls
    }
}
