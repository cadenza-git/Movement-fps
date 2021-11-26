//handles Looking and Shooting, instantiates different things etc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
    public Transform playerBody;    //Reference to transform of the player "collider", for later use in movement
    public float mouseSensitivity = 100.0f; //sens
    public float clampAngle = 87.5f;    //clamp so that player can look further than clampAngle degrees
    private float rotY = 0.0f; 
    private float rotX = 0.0f;
    //Following variables handle Explosion Instantiation, different variables in the process
    public float BoomStrength = 10.0f;
    public float BoomRadius = 5.0f;
    public float BoomLift = 3.0f;
    public float BoomModifier = 1.75f;
    public bool canShoot = true;
    public float coolDown = 5.0f;
    public Rigidbody Reginald;
    public GameObject Particle;
    public GameObject BoomParticle;
    
    void Start()
    {
        //sets out variables so that script can access camera rotation and apply them to the player "collider"
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void CooledDown()
    {
        //Make it so that can only "shoot" in periods
        canShoot = true;
    }
    void SpawnBoom()
    {
        Instantiate(BoomParticle, CastRay().point, Quaternion.Euler(270,0,0));
        Reginald.AddExplosionForce(BoomStrength * BoomModifier, CastRay().point, BoomRadius, BoomLift * BoomModifier);
    }
    
    RaycastHit CastRay()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity);
        return hit;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            
        }
    }
    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))   //attempt at implementation of shift-to-rocketjump from source in unity
        {
            BoomModifier = 1.0f;
        }
        else
        {
            BoomModifier = 1.75f;
        }
        float mouseX = Input.GetAxis("Mouse X");    //Gets mouse input
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime; //Can use mouse input to affect camera  
        rotX += mouseY * mouseSensitivity * Time.deltaTime; 
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);  //Applies the clamping 
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f); //Applies rotation to camera
        transform.rotation = localRotation;
        Vector3 eulerRotation = new Vector3(playerBody.transform.eulerAngles.x, transform.eulerAngles.y, playerBody.transform.eulerAngles.z );
        //^This applies the y rotation to the player so that forces can be applied in same direction as camera
        
        playerBody.rotation = Quaternion.Euler(eulerRotation);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(Particle, CastRay().point, Quaternion.Euler(270,0,0));
              
        }
        
        if (Input.GetKey(KeyCode.Mouse1) && canShoot)
        {
            SpawnBoom();
            canShoot = false;
            Invoke("CooledDown", coolDown);
            //"Rocket Jumping" can only shoot every specific time period
        }
    }
}
