using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp2 : MonoBehaviour
{
    public Transform PickUpPoint;
    private Transform currentItem = null;

    private string pickUpTag = "Seed";

    public float maxPickUpDistance;
    public float capsuleRadius;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (currentItem == null)
            {
                TryPickUpNearbyItem();
            }
            else
            {
                DropItem();
            }
        }

    }

    void TryPickUpNearbyItem()
    {

        Vector3 capsuleStart = transform.position + Vector3.down * capsuleRadius;
        Vector3 capsuleEnd = capsuleStart + transform.forward * maxPickUpDistance;

        RaycastHit[] hits = Physics.CapsuleCastAll(capsuleStart, capsuleEnd, capsuleRadius, transform.forward);

        foreach (RaycastHit hit in hits)
        {
            if (Vector3.Distance(transform.position, hit.transform.position) <= maxPickUpDistance)
            {
                if (hit.transform.CompareTag(pickUpTag))
                {
                    Transform goodItem = null;
                    goodItem = hit.transform;
                    PickUpItem(goodItem);
                    return;
                }
            }
        }
    }

    void PickUpItem(Transform item)
    {
        currentItem = item;
        currentItem.GetComponent<Rigidbody>().useGravity = false;
        currentItem.GetComponent<BoxCollider>().enabled = false;
        currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        currentItem.position = PickUpPoint.position;
        currentItem.SetParent(PickUpPoint);
    }

    void DropItem()
    {

        currentItem.SetParent(null);
        currentItem.GetComponent<Rigidbody>().useGravity = true;
        currentItem.GetComponent<Collider>().enabled = true;
        currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        currentItem = null;
    }
}
