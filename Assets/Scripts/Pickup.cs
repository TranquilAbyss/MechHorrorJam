using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPickedUp = false;
    public Vector3 offset = Vector3.zero;
    public Vector3 pickupEuler = Vector3.zero;
    private Rigidbody rigid;

    private Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        colliders = transform.GetComponentsInChildren<Collider>();
    }

    public void doPickup(Transform transformToFollow)
    {
        isPickedUp = true;
        transform.parent = transformToFollow;
        transform.localPosition = Vector3.zero + offset;
        transform.localEulerAngles = pickupEuler;
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
        rigid.isKinematic = true;
    }

    public void doDrop()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        isPickedUp = false;
        transform.parent = null;
        rigid.isKinematic = false;
    }
}
