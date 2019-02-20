using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int playerNumber;
    [SerializeField] private bool isCarrying = false;
    [SerializeField] private bool carryingPlank = false;
    [SerializeField] private bool carryingCoal = false;
    [SerializeField] private bool carryingAmmo = false;
    private GameObject plankGO;
    private GameObject coalGO;
    private GameObject ammoGO;
    private PlayerMove pm;
    private PlayerShoot ps;

    public enum item { nothing, plank, coal, ammo };
    public item holding = item.nothing;

    private void Awake()
    {
        plankGO = transform.Find("Plank").gameObject;
        coalGO = transform.Find("Coal").gameObject;
        ammoGO = transform.Find("Ammo").gameObject;

        pm = GetComponent<PlayerMove>();
        ps = GetComponent<PlayerShoot>();
    }

    private void Update()
    {
                                                              
        if (isCarrying)
        {
            //slow player move speed (reset in Drop())
            
            pm.SetSpeed(pm.slowMoveSpeed);

            //disable shooting
            ps.enabled= false;

            if (Input.GetButtonDown("joystick " + playerNumber + " B"))
            {
                Drop();
            }

            if(carryingAmmo && Input.GetButtonDown("joystick " + playerNumber + " X"))
            {
                gameObject.GetComponent<PlayerShoot>().Reload();
                Drop();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        // if colliding with boxes and not carrying anything
        if (other.tag == "Plank Box" && !isCarrying)
        {

            //display button sprite
            other.transform.GetComponentInChildren<SpriteRenderer>().enabled = true;

            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                PickUpPlank();
                other.transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
        
        if (other.tag == "Coal Box" && !isCarrying)
        {
            //display button sprite
            other.transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (Input.GetButton("joystick " + playerNumber + " A"))
            {
                PickUpCoal();
                other.transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }

        if (other.tag == "Ammo Box" && !isCarrying)
        {
            //display button sprite
            other.transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (Input.GetButton("joystick " + playerNumber + " A"))
            {
                PickUpAmmo();
                other.transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }

        // Repair pad
        //Repairing
        if (other.tag == "Repair Pad" && !isCarrying)
        {
            RepairPad pad = other.GetComponent<RepairPad>();

                                                                                   
                if (Input.GetButton("joystick " + playerNumber + " X"))
                {
                    pad.Repair();
                }
            
        }
            //Placing planks
            if (other.tag == "Repair Pad" && carryingPlank)
        {
            RepairPad pad = other.GetComponent<RepairPad>();
                                                                                 
           
            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                if (pad.CheckIfCanAdd())
                {
                    pad.AddPlank(1);
                    Drop();
                }
                else return;
            }
        }

        //Furnace 
        if (other.tag == "Furnace" && carryingCoal)
        {
           Furnace fur = other.GetComponent<Furnace>();
   

            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                if (fur.CheckIfCanAdd())
                {
                    fur.AddCoal(1);
                    Drop();
                }
                else return;
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Plank Box" || other.tag == "Coal Box" || other.tag == "Ammo Box")
        {
            //turn off button sprite
            other.transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    void PickUpPlank()
    {
       
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

    void PickUpAmmo()
    {
        ammoGO.SetActive(true);
        isCarrying = true;
        carryingAmmo = true;
    }

    void Drop()
    {
        // if carrying any of these items
        if (carryingPlank || carryingCoal || carryingAmmo)
        {
            
            carryingPlank = false;
            carryingCoal = false;
            carryingAmmo = false;
            carryingCoal = false;
            plankGO.SetActive(false);
            coalGO.SetActive(false);
            ammoGO.SetActive(false);
            isCarrying = false;

            pm.SetSpeed(pm.normalMoveSpeed);
            ps.enabled = true;
        }
    }
}
