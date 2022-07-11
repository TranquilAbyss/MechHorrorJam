using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControls : MonoBehaviour
{
    public Rigidbody rigid;
    public LaserSelect laserSelect;

    public float currentThrottleSpeed = 0;
    public float throttleIncrement = 10;
    public float maxSpeed = 40;
    public float minSpeed = -40;
    public float moveForce = 20;
    public float throttleChangeCooldown = .5f;
    public float currentThrottleChangeTimeStamp = 0;

    public float turnForce = 150;
    public float currentTurnSpeed = 0;
    public float maxTurnSpeed = 0.5f;
    public float minTurnSpeed = -0.5f;

    // Not implemented
    public bool isGrounded = true;

    public bool isCarrying = false;
    public Transform carryPoint;
    public Pickup objectCarried = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Throttle
        if (Input.GetKey(KeyCode.W) && Time.time >= currentThrottleChangeTimeStamp)
        {
            currentThrottleChangeTimeStamp = Time.time + throttleChangeCooldown;
            IncreaseThrottle();
        }
        else if (Input.GetKey(KeyCode.S) && Time.time >= currentThrottleChangeTimeStamp)
        {
            currentThrottleChangeTimeStamp = Time.time + throttleChangeCooldown;
            DecreaseThrottle();
        }

        // Laser Select
        if (Input.GetMouseButtonDown(0))
        {
            if (isCarrying)
            {
                objectCarried.doDrop();
                isCarrying = false;
            }
            else
            {
                if (laserSelect.Hit.transform)
                {
                    if (laserSelect.Hit.transform.tag == "Bolt")
                    {
                        Destroy(laserSelect.Hit.transform.gameObject);
                    }

                    Task task = laserSelect.Hit.transform.GetComponent<Task>();
                    if (task)
                    {
                        GameObject taskObject = laserSelect.Hit.transform.gameObject;

                        if (taskObject.name == "New Generator" && !task.complete && !isCarrying)
                        {
                            isCarrying = true;
                            objectCarried = taskObject.GetComponent<Pickup>();
                            objectCarried.doPickup(carryPoint);
                        }
                    }
                }
            }
        }
    }

    void IncreaseThrottle()
    {
        currentThrottleSpeed += throttleIncrement;
        if (currentThrottleSpeed > maxSpeed)
        {
            currentThrottleSpeed = maxSpeed;
        }
    }

    void DecreaseThrottle()
    {
        currentThrottleSpeed += -throttleIncrement;
        if (currentThrottleSpeed < minSpeed)
        {
            currentThrottleSpeed = minSpeed;
        }
    }
    
    void FixedUpdate()
    {
        //TODO grounded check 
        if (isGrounded)
        {
            if (currentThrottleSpeed > 0 && rigid.velocity.magnitude < currentThrottleSpeed)
            {
                rigid.AddForce(moveForce * transform.forward);
            }
            else if (currentThrottleSpeed < 0 && rigid.velocity.magnitude > currentThrottleSpeed)
            {
                rigid.AddForce(-moveForce * transform.forward);
            }

            // turning
            if (rigid.angularVelocity.y < maxTurnSpeed && rigid.angularVelocity.y > minTurnSpeed)
            {
                float preturnMagnitude = new Vector2(rigid.velocity.x, rigid.velocity.z).magnitude;
                
                rigid.AddTorque(Input.GetAxis("Horizontal") * turnForce * transform.up);
                currentTurnSpeed = rigid.angularVelocity.y;

                //eliminates sliding during turns while maintiain speed.
                Vector3 localVelocity = rigid.transform.InverseTransformDirection(rigid.velocity);    
                rigid.velocity = transform.forward * preturnMagnitude * Mathf.Sign(localVelocity.z) + new Vector3(0, rigid.velocity.y, 0);
            }
        }
    }
}
