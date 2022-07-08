using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControls : MonoBehaviour
{
    public Rigidbody rigid;

    private float velocityMagnitudeLimit = 0;
    public float turnForce = 150;
    public float maxTurnSpeed = 0.5f;
    public float throttleChange = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO max throttle limit
        if (Input.GetKeyDown(KeyCode.W))
        {
            velocityMagnitudeLimit += throttleChange;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            velocityMagnitudeLimit += -throttleChange;
        }
    }
    
    void FixedUpdate()
    {
        //TODO if ground check 
        if (true)
        {
            // throttle
            if (velocityMagnitudeLimit > 0 && rigid.velocity.magnitude < velocityMagnitudeLimit)
            {
                rigid.AddForce(throttleChange * transform.forward);
            }
            else if (velocityMagnitudeLimit < 0 && rigid.velocity.magnitude > velocityMagnitudeLimit)
            {
                rigid.AddForce(-throttleChange * transform.forward);
            }

            // turning
            if (rigid.angularVelocity.y < maxTurnSpeed && rigid.angularVelocity.y > -maxTurnSpeed)
            {
                rigid.AddTorque(Input.GetAxis("Horizontal") * turnForce * transform.up);
            }
        }
    }
}
