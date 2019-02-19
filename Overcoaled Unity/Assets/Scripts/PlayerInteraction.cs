using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool isCarrying = false;
    [SerializeField] private bool carryingPlank = false;
    [SerializeField] private bool carryingCoal = false;
    [SerializeField] private bool carryingAmmo = false;
    private GameObject plankGO;
    private GameObject coalGO;

    private void Awake()
    {
        plankGO = transform.Find("Plank").gameObject;
        coalGO = transform.Find("Coal").gameObject;
    }

    private void Update()
    {

                                                                                // ONLY FOR TESTING
        if (isCarrying)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Drop();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // if colliding with plank box and not carrying anything
        if(other.tag == "Plank Box" && !isCarrying)
        {
                                                                               //CHANGE THIS INPUT
            if (Input.GetMouseButtonDown(0))
            {
                PickUpPlank();
            }
        }

        // Repair pad
        //Repairing
        if (other.tag == "Repair Pad" && !isCarrying)
        {
            RepairPad pad = other.GetComponent<RepairPad>();

                                                                                   // CHANGE THIS INPUT
                if (Input.GetMouseButton(1))
                {
                    pad.Repair();
                }
            
        }
            //Placing planks
            if (other.tag == "Repair Pad" && carryingPlank)
        {
            RepairPad pad = other.GetComponent<RepairPad>();
                                                                                   //CHANGE THIS INPUT
           
            if (Input.GetMouseButtonDown(0))
            {
                if (pad.CheckIfCanAdd())
                {
                    pad.AddPlank(1);
                    Drop();
                }
                else return;
            }
        }


    }

    void PickUpPlank()
    {
        print("picked up plank");
        plankGO.SetActive(true);
        isCarrying = true;
        carryingPlank = true;
    }

    void PickUpCoal()
    {
        coalGO.SetActive(true);
        isCarrying = true;
        carryingCoal = true;
    }

    void Drop()
    {
        // if carrying any of these items
        if (carryingPlank || carryingCoal || carryingAmmo)
        {
            
            carryingPlank = false;
            carryingCoal = false;
            carryingAmmo = false;
            plankGO.SetActive(false);
            coalGO.SetActive(false);
            isCarrying = false;
        }
    }
}
