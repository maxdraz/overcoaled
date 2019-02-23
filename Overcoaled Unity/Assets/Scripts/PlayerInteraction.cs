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
    private GameObject plankHolder;
    private GameObject coalHolder;
    private GameObject ammoHolder;
    private PlayerMove pm;
    private PlayerShoot ps;
    private ParticleSystem particle;

    [SerializeField] private float level1ThrowTime;
    
    [SerializeField] private float level3ThrowTime;
    [SerializeField] private float maxThrowTime;
    private bool countUp = true;

    [SerializeField] private GameObject plankPrefab;
    [SerializeField] private GameObject coalPrefab;

    public enum item { nothing, plank, coal, ammo };
    public item holding = item.nothing;

    public float throwTimer = 0;

    private Transform throwChargesHolder;
   

    private void Awake()
    {
        plankHolder = transform.Find("Plank").gameObject;
        coalHolder = transform.Find("Coal").gameObject;
        ammoHolder = transform.Find("Ammo").gameObject;
        particle = transform.GetComponentInChildren<ParticleSystem>();
        throwChargesHolder = transform.Find("ThrowCharges");
        
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
            // pause particle system
            particle.Stop();

            if (Input.GetButtonDown("joystick " + playerNumber + " B"))
            {
                Drop();
            }

            if(carryingAmmo && Input.GetButtonDown("joystick " + playerNumber + " X"))
            {
                gameObject.GetComponent<PlayerShoot>().Reload();
                Drop();
            }

            //Throwing
            if(Input.GetButton("joystick " + playerNumber + " X"))
            {

                if (countUp)
                {
                    throwTimer += Time.deltaTime;

                    if(throwTimer >= maxThrowTime)
                    {
                        countUp = false;
                    }

                }

                if (!countUp)
                {
                    throwTimer -= Time.deltaTime;
                    
                    if(throwTimer <= 0)
                    {
                        countUp = true;
                    }
                }
                
                if(throwTimer <= level1ThrowTime)
                {
                    throwChargesHolder.transform.GetChild(0).gameObject.SetActive(true);
                }
                
                if (throwTimer >level1ThrowTime)
                {
                    throwChargesHolder.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    throwChargesHolder.transform.GetChild(1).gameObject.SetActive(false);
                }
                if (throwTimer >= level3ThrowTime)
                {
                    throwChargesHolder.transform.GetChild(2).gameObject.SetActive(true);
                }

                else
                {                    
                    throwChargesHolder.transform.GetChild(2).gameObject.SetActive(false);
                }

                

            }


            else
            {
                
                if (Input.GetButtonUp("joystick " + playerNumber + " X"))
                {
                    print("let go after " + throwTimer);
                    
                    if(throwTimer >= level3ThrowTime)
                    {
                        Throw(3);
                    } else if(throwTimer > level1ThrowTime)
                    {
                        Throw(2);
                    }
                    else if (throwTimer <= level1ThrowTime)
                    {
                        Throw(1);
                    }

                    throwTimer = 0;
                    throwChargesHolder.transform.GetChild(0).gameObject.SetActive(false);
                    throwChargesHolder.transform.GetChild(1).gameObject.SetActive(false);
                    throwChargesHolder.transform.GetChild(2).gameObject.SetActive(false);
                }
                
                
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

    private void OnCollisionStay(Collision collision)
    {
        // Picking stuff off ground
        if(collision.gameObject.tag == "Plank" && !isCarrying)
        {
               if (Input.GetButtonDown("joystick " + playerNumber + " A"))
                {
                    PickUpPlank();
                Destroy(collision.gameObject);
                }
            
        }

        if (collision.gameObject.tag == "Coal" && !isCarrying)
        {
            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                PickUpCoal();
                Destroy(collision.gameObject);
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
       
        plankHolder.SetActive(true);
        isCarrying = true;
        carryingPlank = true;
    }

    void PickUpCoal()
    {
        coalHolder.SetActive(true);
        isCarrying = true;
        carryingCoal = true;
    }

    void PickUpAmmo()
    {
        ammoHolder.SetActive(true);
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
            plankHolder.SetActive(false);
            coalHolder.SetActive(false);
            ammoHolder.SetActive(false);
            isCarrying = false;

            pm.SetSpeed(pm.normalMoveSpeed);
            ps.enabled = true;
            particle.Play();
        }
    }

    void Throw(int forceLevel)
    {
        if (carryingPlank)
        {
            
            GameObject plankGO = (GameObject)Instantiate(plankPrefab, plankHolder.transform.position, plankHolder.transform.rotation);
            ThrownObjectMove move = plankGO.GetComponent<ThrownObjectMove>();
            move.setForceLevel(forceLevel);
            move.Move();
            Drop();
        }

        if (carryingCoal)
        {
            GameObject coalGO = (GameObject)Instantiate(coalPrefab, plankHolder.transform.position, plankHolder.transform.rotation);
            ThrownObjectMove move = coalGO.GetComponent<ThrownObjectMove>();
            move.setForceLevel(forceLevel);
            move.Move();
            Drop();
        }
    }
}
