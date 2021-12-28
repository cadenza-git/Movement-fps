using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;    //Reference to transform of the player "collider", for later use in movement
    public Grapple GrappleScript;
    private bool IsGrappling;

    [Header("Audio/Animation")]
    public AudioSource DoorOpens;
    public Animator VaultDoor;
    [Space]
    
    [Header("UI/Text")]
    public Text KillObj;
    public Text AltObj;
    public Text ParkObj;
    public TextMeshPro Text;
    public GameObject GetToBox;
    [Space]
	
	[Header("Looking")]
    public float mouseSensitivity = 100.0f; //sens
    public float clampAngle = 87.5f;    //clamp so that player can look further than clampAngle degrees
    private float rotY = 0.0f; 
    private float rotX = 0.0f;
    public GameObject CheckSphere;
    private JumpDetect jumpDetect;
    private bool LeanCamera;
    private float LeanAmount = 0.0f;
    private Camera camera;
    [Space]
    
    
    [Header("Enemies")]

    public GameObject NewEnemy;
    private bool HasKilled;
    private int layerMask;
  
    [Space]
    
    [Header("Levels")]
    
    public Collider Pass;
    public int DoorMove;
    public bool check1;
    public bool check2;
    public bool check3;
    public GameObject NextLevelInhibit;
    
    
    
    void Start()
    {

        jumpDetect = CheckSphere.GetComponent<JumpDetect>();
        
        camera = GetComponent<Camera>();
        
        //sets out variables so that script can access camera rotation and apply them to the player "collider"
        if(SceneManager.GetActiveScene().buildIndex == 6)
        {
            GetToBox.SetActive(false);
        }
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    
    void SpawnEnemy()
    {
        
        Instantiate(NewEnemy, new Vector3(47.85f, -7.45f, -32.56f),  Quaternion.identity);
        
    }
    
    
    RaycastHit CastRay()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity, layerMask);
        return hit;
    }
    
    public void OnDeath()
    {
        DoorMove++;
    }
    
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");    //Gets mouse input
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime; //Can use mouse input to affect camera  
        rotX += mouseY * mouseSensitivity * Time.deltaTime; 
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);  //Applies the clamping 
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, LeanAmount); //Applies rotation to camera
        transform.rotation = localRotation;
        Vector3 eulerRotation = new Vector3(playerBody.transform.eulerAngles.x, transform.eulerAngles.y, playerBody.transform.eulerAngles.z );
        //^This applies the y rotation to the player so that forces can be applied in same direction as camera
        playerBody.rotation = Quaternion.Euler(eulerRotation);
    }


    void Update()
    {
    	

        IsGrappling = GrappleScript.IsCurrGrappling;


        if(jumpDetect.TellCameraLean || IsGrappling)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 141.5f, 0.2f);
        }
        else if(!jumpDetect.TellCameraLean || !IsGrappling)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 130f, 0.2f);
        }


        LeanCamera = jumpDetect.TellCameraLean;
        if(LeanCamera)
        {
            if(jumpDetect.SideOfWallRun == "left")
            {
                LeanAmount = Mathf.Lerp(LeanAmount, -11.5f, 0.25f);
            }
            else if(jumpDetect.SideOfWallRun == "right")
            {
                
               LeanAmount = Mathf.Lerp(LeanAmount, 11.5f, 0.25f);
                
            }



        }
        else
        {
            LeanAmount = 0.0f;
        }
        
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            if(DoorMove == 2)
            {
                VaultDoor.Play("Base Layer.Down",0,0);
                DoorOpens.Play();
            
            }
            if(DoorMove == 3)
            {
                DestroyImmediate(Text);
            }
            
            Text.text = "Kill 3 ye please.         Killed now: " + DoorMove;
        }
        
        layerMask = 1 << 3;
        layerMask = ~layerMask;
        
	
        if(SceneManager.GetActiveScene().buildIndex == 6)
        {  
            if(check2)
            {
                KillObj.text = "Done. Good";
            }
            if(check3)
            {
                AltObj.text = "Job Good";
            }
            
            if(check1)
            {
                ParkObj.text = "Exercise Done";
            }
            
            if(DoorMove >= 0)
            {   
                Debug.Log("2 is activated");                
                check2 = true;
            }
            if (check1 && check2 && check3)
            {
                
                GetToBox.SetActive(true);
                NextLevelInhibit.GetComponent<Collider>().enabled = false;
                NextLevelInhibit.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit = CastRay();
            if(SceneManager.GetActiveScene().buildIndex == 6)
            {
                if (hit.distance<5 && hit.collider.name == "Check1")//arbitrary name
                {   
                    Debug.Log("1 is activated");
                    check1 = true;
                }
                
                if (hit.distance<5 && hit.collider.name == "Check2")//arbitrary name
                {
                    if(HasKilled)
                    {
                        SpawnEnemy();
                        HasKilled = false;
                    }   
                }
                
                if (hit.distance<5 && hit.collider.name == "Check3")//arbitrary name
                {   
                    Debug.Log("3 is activated");
                    check3 = true;
                }
                
            }
            
            if (hit.distance<5 && hit.collider.name == "Interacube")//arbitrary name
            {
                Debug.Log("Interacted");
                SpawnEnemy();  
                
            }
        }
    }
}
