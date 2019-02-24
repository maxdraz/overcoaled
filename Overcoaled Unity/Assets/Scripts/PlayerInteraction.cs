using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int playerNumber;
    [SerializeField] private int noPlayerCollisionLayer;
    [SerializeField] private bool isCarrying = false;
    [SerializeField] private bool carryingPlank = false;
    [SerializeField] private bool carryingCoal = false;
    [SerializeField] private bool carryingGun = false;
    [SerializeField] private bool carryingPlayer = false;
    private GameObject plankHolder;
    private GameObject coalHolder;
    private GameObject gunHolder;
    private GameObject playerHolder;
    
    private PlayerMove pm;
    private PlayerShoot ps;
    private ParticleSystem particle;

    [SerializeField] private float level1ThrowTime;
    
    [SerializeField] private float level3ThrowTime;
    [SerializeField] private float maxThrowTime;
    private bool countUp = true;

    [SerializeField] private GameObject plankPrefab;
    [SerializeField] private GameObject coalPrefab;
    [SerializeField] private GameObject gunPrefab;

    public enum item { nothing, plank, coal, ammo };
    public item holding = item.nothing;

    public float throwTimer = 0;

    private Transform throwChargesHolder;
    ThrownObjectMove myPlayerThrow;
   

    private void Awake()
    {
        plankHolder = transform.Find("Plank").gameObject;
        coalHolder = transform.Find("Coal").gameObject;
        gunHolder = transform.Find("Gun").gameObject;
        particle = transform.GetComponentInChildren<ParticleSystem>();
        throwChargesHolder = transform.Find("ThrowCharges");
        playerHolder = transform.Find("PlayerHolder").gameObject;
        
        pm = GetComponent<PlayerMove>();
        ps = GetComponent<PlayerShoot>();
        ps.enabled = false;
        myPlayerThrow = GetComponent<ThrownObjectMove>();
        
    }

    private void OnEnable()
    {
        myPlayerThrow.enabled = false;
    }

    private void Update()
    {
                                                              
        if (isCarrying)
        {
            //slow player move speed (reset in Drop())
            
            pm.SetSpeed(pm.slowMoveSpeed);

            //disable shooting
            if (!carryingGun)
            {
                ps.enabled = false;
            }
            // pause particle system
            particle.Stop();

            if (Input.GetButtonDown("joystick " + playerNumber + " B"))
            {
                Drop();
            }

            //if(carryingGun && Input.GetButtonDown("joystick " + playerNumber + " X"))
            //{
            //    //gameObject.GetComponent<PlayerShoot>().Reload(); 
                
            //    Drop();
            //}

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

        if (other.tag == "Gun Box" && !isCarrying)
        {
            //display button sprite
            other.transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (Input.GetButton("joystick " + playerNumber + " A"))
            {
                PickUpGun();
                ps.Reload();
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

        if (collision.gameObject.tag == "Gun" && !isCarrying)
        {
            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                PickUpGun();
                ps.ammo = collision.gameObject.GetComponent<Gun>().ammoInGun;
                Destroy(collision.gameObject);
            }

        }

        if (collision.gameObject.tag == "Player" && !isCarrying)
        {
            if (Input.GetButtonDown("joystick " + playerNumber + " A"))
            {
                PickUpPlayer(collision);
                
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Plank Box" || other.tag == "Coal Box" || other.tag == "Gun Box")
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

    void PickUpGun()
    {
        gunHolder.SetActive(true);
        isCarrying = true;
        carryingGun = true;
        ps.enabled = true;
    }

    void PickUpPlayer(Collision col)
    {
        col.gameObject.GetComponent<PlayerMove>().enabled = false;
        col.gameObject.layer = noPlayerCollisionLayer;
        col.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        col.collider.enabled = false;
        col.transform.position = playerHolder.transform.position;
        col.transform.rotation = playerHolder.transform.rotation;
        col.transform.parent = playerHolder.transform;
        


        isCarrying = true;
        carryingPlayer = true;
        
    }

    void Drop()
    {
        // if carrying any of these items
        if (carryingPlank || carryingCoal || carryingGun)
        {
            
            carryingPlank = false;
            carryingCoal = false;
            carryingGun = false;
            carryingCoal = false;
            plankHolder.SetActive(false);
            coalHolder.SetActive(false);
            gunHolder.SetActive(false);
            isCarrying = false;

            pm.SetSpeed(pm.normalMoveSpeed);
            ps.enabled = false;
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
        if (carryingGun)
        {
            GameObject gunGO = (GameObject)Instantiate(gunPrefab, plankHolder.transform.position, plankHolder.transform.rotation);
            gunGO.GetComponent<Gun>().SetAmmo(ps.ammo);
            ThrownObjectMove move = gunGO.GetComponent<ThrownObjectMove>();
            move.setForceLevel(forceLevel);
            move.Move();
            Drop();

        }

        if (carryingPlayer)
        {
            // undo all the stuff and unparent
            // only setactive scripts when grounded (do this in thrown objects move script)
            // Add force
        }
    }
}
