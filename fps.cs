using UnityEngine;
using UnityEngine.UI;
 
public class fps : MonoBehaviour
{
    public GameObject FPSCAM;
    private MouseLook Mlook;
    public int avgFrameRate;
    public float ShowSpeed;
    public MeshRenderer EnableShootTell;
    public Text ShootTell;
    public Text display_Text;
    public Text SpeedText;
    public Rigidbody player;
    private string TellMePls;
    
    public void Start ()
    {
        Mlook = FPSCAM.GetComponent<MouseLook>();
        //GetComponent(MeshRenderer).enabled = false;
    }
    public void Update ()
    {
        //Mlook.canShoot
        float current = 0;
        float ShowSpeed = player.velocity.magnitude;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS ";
        SpeedText.text = ShowSpeed.ToString();
        if (!Mlook.canShoot)
        {
            //EnableShootTell.enabled = false;
            TellMePls = "No";
        }
        else
        {
            //EnableShootTell.enabled = true;
            TellMePls = "Yes";
        }
        ShootTell.text = TellMePls;
    }
}
