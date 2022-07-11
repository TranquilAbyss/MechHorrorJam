using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropField : MonoBehaviour
{
    public GameObject target;
    private Rigidbody colliderRigid;
    private MeshRenderer meshRenderer;
    private Task taskCompleted; // task completed by field

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        // wait for object to touch ground
        if (colliderRigid && colliderRigid.velocity.y == 0)
        {
            colliderRigid.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == target.tag)
        {
            // handle pickupable objects
            Pickup objectPickupable;
            objectPickupable = other.GetComponent<Pickup>();
            if (!objectPickupable)
            {
                objectPickupable = other.GetComponentInParent<Pickup>();
            }
            
            if (objectPickupable && !objectPickupable.isPickedUp)
            {
                meshRenderer.enabled = false;
                colliderRigid = objectPickupable.gameObject.GetComponent<Rigidbody>();
                colliderRigid.transform.localEulerAngles = transform.eulerAngles;
                colliderRigid.transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
                colliderRigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                objectPickupable.enabled = false;
                if (taskCompleted)
                {
                    taskCompleted.CompleteTask();
                }
            }
        }
    }
}
