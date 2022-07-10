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
    
    public float turnForce = 150;
    public float currentTurnSpeed = 0;
    public float maxTurnSpeed = 0.5f;
    public float minTurnSpeed = -0.5f;
    public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Throttle
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentThrottleSpeed += throttleIncrement;
            if (currentThrottleSpeed > maxSpeed)
            {
                currentThrottleSpeed = maxSpeed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentThrottleSpeed += -throttleIncrement;
            if (currentThrottleSpeed < minSpeed)
            {
                currentThrottleSpeed = minSpeed;
            }
        }

        // Laser Select
        if (Input.GetMouseButtonDown(0))
        {
            if (laserSelect.Hit.transform)
            {
                Task task = laserSelect.Hit.transform.GetComponent<Task>();
                if (task)
                {
                    GameObject taskObject = laserSelect.Hit.transform.gameObject;
                    Debug.Log(taskObject);
                }

            }
        }
    }
    
    void FixedUpdate()
    {
        //TODO grounded check 
        if (isGrounded)
        {
            // throttle
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
