using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool isCarrying = false;
    [SerializeField] private bool carryingPlank = false;
    [SerializeField] private bool carryingCoal = false;
    [SerializeField] private bool carryingAmmo = false;

    private void OnTriggerEnter(Collider other)
    {
        // if colliding with plank box and not carrying anything
        if(other.tag == "Plank Box" && !isCarrying)
        {
            PickUpPlank();
        }


    }

    void PickUpPlank()
    {
        isCarrying = true;
        carryingPlank = true;
    }
}
