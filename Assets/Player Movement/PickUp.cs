using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform PickUpPoint;
    private Transform currentItem;
    private string pickUpTag = "Seed";
    private string seedBox = "getseed";
    private string harvestBox = "getharvest";

    public float maxPickUpDistance;
    public float capsuleRadius;

    private LayerMask ground;

    public GameObject cornSeed;
    public GameObject wheatSeed;
    public GameObject tomSeed;
    public GameObject rawPot;


    public GameObject wheatfruit;
    public GameObject tomfruit;
    public GameObject cornfruit;

    // Reference to the CashCounter script
    public CashCounter cashCounter;

    void Start()
    {
        ground = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                    Transform goodItem = hit.transform;
                    PickUpItem(goodItem);
                    return;
                }
                else if (hit.transform.CompareTag(seedBox))
                {
                    Debug.Log("Get seeds");
                    // Create seed
                    string boxtype = hit.transform.gameObject.GetComponent<PlaceObj>().landType;
                    if(boxtype == "cornseeds"){
                        PickUpItem(Instantiate(cornSeed).transform);
                    } else if(boxtype == "wheatseeds"){
                        PickUpItem(Instantiate(wheatSeed).transform);
                    } else if(boxtype == "tomseeds"){
                        PickUpItem(Instantiate(tomSeed).transform);
                    }
                    return;
                }
                else if (hit.transform.CompareTag(harvestBox))
                {

                    string fruitType = hit.transform.GetComponent<PlaceObj>().harvestFruit;
                    if (fruitType == "wheat")
                    {
                        PickUpItem(Instantiate(wheatfruit).transform);
                    }
                    else if (fruitType == "corn")
                    {
                        PickUpItem(Instantiate(cornfruit).transform);
                    }
                    else if (fruitType == "tom")
                    {
                        PickUpItem(Instantiate(tomfruit).transform);
                    }
                    //create new pot
                    Instantiate(rawPot).transform.position = hit.transform.position;
                    //delete old pot
                    Destroy(hit.transform.gameObject);

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
        // Check the ground where it will be placed
        RaycastHit hit;

        // Cast a ray downwards from the pick-up point
        if (Physics.Raycast(PickUpPoint.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // Attempt to interact with the object below
            var placeObjComponent = hit.transform.GetComponent<PlaceObj>();
            if (placeObjComponent != null)
            {
                int value = placeObjComponent.Special(currentItem.GetComponent<objValues>().holdingType);

                if(value == 1){
                    
                // Successfully placed item
                Destroy(currentItem.gameObject);
                } else if (value == 2){
                    // water
                    currentItem.GetChild(0).gameObject.SetActive(false);
                    currentItem.GetChild(1).gameObject.SetActive(true);
                }else if (value == 3){
                    // empty water
                    currentItem.GetChild(1).gameObject.SetActive(false);
                    currentItem.GetChild(0).gameObject.SetActive(true);
                }else if (value == 4){
                    // sell
                    
                    //NEW MO's
                    string itemType = currentItem.GetComponent<objValues>().holdingType;
                    Debug.Log("sell");
                    int cropValue = 0;
                    if (itemType == "tomCrate")
                    {
                        cropValue = 20;
                    }
                    else if (itemType == "cornCrate")
                    {
                        cropValue = 15;

                    }
                    else if (itemType == "wheatCrate")
                    {
                        cropValue = 10;

                    }
                    cashCounter.AddCash(cropValue); // Add cash for selling the item
                    //Debug.Log($"Sold {itemType} for ${cropValue}");
                    // Get rid of the box
                    Destroy(currentItem.gameObject);
                }

            }
            else
            {
                // Drop the item normally
                currentItem.SetParent(null);
                currentItem.GetComponent<Rigidbody>().useGravity = true;
                currentItem.GetComponent<Collider>().enabled = true;
                currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                currentItem = null;
            }
        }
    }
}