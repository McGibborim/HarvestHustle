using Unity.VisualScripting;
using UnityEngine;

public class PlaceObj : MonoBehaviour
{
    public string landType; // Type of land (e.g., "farm")

    // Prefabs for pots with different plants
    [SerializeField] private GameObject wheatPotPrefab;   // Pot with wheat plant
    [SerializeField] private GameObject tomatoPotPrefab;  // Pot with tomato plant
    [SerializeField] private GameObject cornPotPrefab;    // Pot with corn plant
    [SerializeField] public string harvestFruit;


    public int Special(string holding)
    {
        // Only proceed if it's farmland
        if (landType == "farm")
        {
            GameObject newPotPrefab = null;

            // Check which seed is being held and set the appropriate pot prefab
            if (holding == "seed_w")
            {
                newPotPrefab = wheatPotPrefab;
            }
            else if (holding == "seed_t")
            {
                newPotPrefab = tomatoPotPrefab;
            }
            else if (holding == "seed_c")
            {
                newPotPrefab = cornPotPrefab;
            }

            // If a matching pot prefab is found, replace the current pot
            if (newPotPrefab != null)
            {
                // Instantiate(newPotPrefab, transform.position, Quaternion.Euler(-90, 0, 0)); // Instantiate with correct rotation
                Instantiate(newPotPrefab).transform.position = transform.position; // Instantiate with correct rotation
                Destroy(this.gameObject); // Destroy the current pot

                return 1; // Indicate success
            }
        }
        if(holding=="bucketwater"){
            Debug.Log("water");
            GameObject newPotPrefab = null;

            if(landType=="c_water"){
                newPotPrefab = cornPotPrefab;


            } else if(landType=="w_water"){
                newPotPrefab = wheatPotPrefab;


            }else if(landType=="t_water"){
                newPotPrefab = tomatoPotPrefab;


            }
            if (newPotPrefab != null)
            {
                // Instantiate(newPotPrefab, transform.position, Quaternion.Euler(-90, 0, 0)); // Instantiate with correct rotation
                Transform newPot = Instantiate(newPotPrefab).transform;
                newPot.position = transform.position; // Instantiate with correct rotation
                if(landType=="t_water"){
                    newPot.position += new Vector3(0, 2f, 1);
                }
                Destroy(this.gameObject); // Destroy the current pot

                return 3; // Indicate success
            }
            
        }
        
        // return 3 to switch back to empty water bucket trey.

        if(landType=="water"){
            if(holding=="bucket"){
                Debug.Log("bucket");
                return 2;
            }else if (holding =="bucketwater"){
                // do nothing because it already has the water
                return 0;
            }
        }

        if(landType=="shop"){
            if(holding=="tomCrate"||holding=="cornCrate"||holding=="wheatCrate"){
                return 4;
            }
        }

        // Return 0 if conditions were not met
        return 0;
    }
}