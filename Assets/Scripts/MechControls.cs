using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControls : MonoBehaviour
{
    public Rigidbody rigid;
    public LaserSelect laserSelect;
    public Animator anim;
    public Light light;
    public GameObject[] screens;

    public float currentThrottleSpeed = 0;
    public float throttleIncrement = 10;
    public float maxSpeed = 40;
    public float minSpeed = -40;
    public float moveForce = 20;
    public float throttleChangeCooldown = .5f;
    public float currentThrottleChangeTimeStamp = 0;

    public float turnForce = 150;
    public float currentTurnSpeed = 0;
    public float currentTurnThrottled = 0;
    public float maxTurnSpeed = 0.5f;
    public float minTurnSpeed = -0.5f;

    // Not implemented
    public bool isGrounded = true;

    public bool isCarrying = false;
    public Transform carryPoint;
    public Pickup objectCarried = null;
    public float intractDistance = 15;

    //Sounds
    public AudioClip collisionSound;
    public AudioSource rightFootSound;
    public AudioSource leftFootSound;
    public AudioSource interactSound;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void PlayRightFootSound()
    {
        if(rigid.velocity.magnitude > 0.1f || Mathf.Abs(rigid.angularVelocity.y) > .1f)
        rightFootSound.Play();
    }

    public void PlayLeftFootSound()
    {
        if (rigid.velocity.magnitude > 0.1f || Mathf.Abs(rigid.angularVelocity.y) > .1f)
            leftFootSound.Play();
    }

    public void StartMarch()
    {
        StartCoroutine(StartMechRoutine());
    }

    IEnumerator StartMechRoutine()
    {
        yield return new WaitForSeconds(.5f);
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(.4f,.7f));
        }
        yield return new WaitForSeconds(1f);
        light.enabled = true;
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
        currentTurnThrottled = Input.GetAxis("Horizontal");

        // Laser Color
        if (laserSelect.Hit.transform)
        {
            Pickup pickable = laserSelect.Hit.transform.GetComponent<Pickup>();
            Interact button = laserSelect.Hit.transform.GetComponent<Interact>();
            if (((pickable && pickable.enabled) || 
                (button && button.enabled)) &&
                laserSelect.Hit.distance <= intractDistance)
            {
                // only run function if target is currently not hit.
                if (!laserSelect.isTargetHit)
                {
                    laserSelect.ChangeToHitTargetColor();
                }
            }
            else if (laserSelect.isTargetHit)
            {
                laserSelect.ChangeToDefaultColor();
            }
        }

        // Laser Select
        if (Input.GetMouseButtonDown(0))
        {
            if (isCarrying)
            {
                objectCarried.doDrop();
                isCarrying = false;
                objectCarried = null;
                interactSound.Play();
            }
            else
            {
                if (laserSelect.Hit.transform && laserSelect.Hit.distance <= intractDistance)
                {
                    Interact interactable = laserSelect.Hit.transform.GetComponent<Interact>();
                    //Interact
                    if (interactable)
                    {
                        interactable.doInteract();
                        interactSound.Play();
                    }

                    // Bolts
                    if (laserSelect.Hit.transform.tag == "Bolt")
                    {
                        Destroy(laserSelect.Hit.transform.gameObject);
                    }
                    
                    // Carry 
                    if (!isCarrying && laserSelect.Hit.transform.GetComponent<Pickup>())
                    {
                        objectCarried = laserSelect.Hit.transform.GetComponent<Pickup>();
                        isCarrying = true;
                        objectCarried.doPickup(carryPoint);
                        interactSound.Play();
                        Task task = laserSelect.Hit.transform.GetComponent<Task>();
                        if (task && !task.complete)
                        {
                            task.CompleteTask();
                        }
                    }
                    else
                    {
                        Debug.Log(laserSelect.Hit.transform.name + " does not have Pickup script");
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

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(collisionSound, collision.GetContact(0).point);
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
            else if (currentThrottleSpeed < 0 && rigid.velocity.magnitude < Mathf.Abs(currentThrottleSpeed))
            {
                rigid.AddForce(-moveForce * transform.forward);
            }

            // turning
            if (rigid.angularVelocity.y < maxTurnSpeed && rigid.angularVelocity.y > minTurnSpeed)
            {
                float preturnMagnitude = new Vector2(rigid.velocity.x, rigid.velocity.z).magnitude;

                rigid.AddTorque(currentTurnThrottled * turnForce * transform.up);
                currentTurnSpeed = rigid.angularVelocity.y;

                //eliminates sliding during turns while maintiain speed.
                Vector3 localVelocity = rigid.transform.InverseTransformDirection(rigid.velocity);
                rigid.velocity = transform.forward * preturnMagnitude * Mathf.Sign(localVelocity.z) + new Vector3(0, rigid.velocity.y, 0);
            }

            anim.SetFloat("Blend", Mathf.Abs((rigid.angularVelocity.y / (maxTurnSpeed * 2))) + (rigid.velocity.magnitude / maxSpeed));
        }
    }
}
