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
    private float start = 0.01f;
    private float period = 0.1f;
    
    public void Start ()
    {
        Mlook = FPSCAM.GetComponent<MouseLook>(); //References canShoot from MouseLook for use.
        LifeScript = Player.GetComponent<NewBehaviourScript>();
        InvokeRepeating("HUD", start, period);//updates the HUD every period seconds
    }
    
    void HUD()
    {
    
        float ShowSpeed = player.velocity.magnitude;
        int IntSpeed = (int)Mathf.Round(ShowSpeed);
        float current = 0;
        
        current = (int)(1f / Time.unscaledDeltaTime);//DeltaTime returns a float between 0 and 1, related to fps, divide 1 by it to get proper number
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS ";
        
        SpeedText.text = IntSpeed.ToString();
    }
    
    public void FixedUpdate ()
    {
        
        LifeText.text = LifeScript.life.ToString; //displays how much "life" is left
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
