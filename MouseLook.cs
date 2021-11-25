using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 87.5f;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    public float BoomStrength = 10.0f;
    public float BoomRadius = 5.0f;
    public float BoomLift = 3.0f;
    public float BoomModifier = 1.75f;
    public bool canShoot = true;
    public float coolDown = 5.0f;
    public Rigidbody Reginald;
    public Transform playerBody;
    public GameObject Particle;
    public GameObject BoomParticle;
    
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void CooledDown()
    {
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
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            BoomModifier = 1.0f;
        }
        else
        {
            BoomModifier = 1.75f;
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
        Vector3 eulerRotation = new Vector3(playerBody.transform.eulerAngles.x, transform.eulerAngles.y, playerBody.transform.eulerAngles.z );
        
        playerBody.rotation = Quaternion.Euler(eulerRotation);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(Particle, CastRay().point, Quaternion.Euler(270,0,0));
            
            //Instantiate(Bullet,);
            //Destroy(Particle);
        }
        
        if (Input.GetKey(KeyCode.Mouse1) && canShoot)
        {
            //Instantiate(BoomParticle, CastRay().point, Quaternion.Euler(270,0,0));
            //Reginald.AddExplosionForce(BoomStrength, CastRay().point, BoomRadius, BoomLift);
            SpawnBoom();
            canShoot = false;
            Invoke("CooledDown", coolDown);
            //Instantiate(Bullet,);
            //Destroy(Particle);
        }
    }
}
