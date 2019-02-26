using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPad : MonoBehaviour
{
    private TextMesh plankText;
    private TextMesh repairText;
    public enum Item { far, farWindow, near, nearWindow}
    public Item wallType;
    public float plankCount;
   [SerializeField]
    private float maxPlanks = 3 ;
    [SerializeField]
    private GameObject wallFarPrefab;
    [SerializeField]
    private GameObject wallFarWindowPrefab;
    [SerializeField]
    private GameObject wallNearPrefab;
    [SerializeField]
    private GameObject wallNearWindowPrefab;
    private Vector3 wallSpawnOffset = new Vector3(0,0.5f,0);
    [SerializeField]
    private float repairCD = 4f;
    [SerializeField]
    private float repairCDRemaining;
    private TextMesh[] texts;

    private void Awake()
    {

        texts = GetComponentsInChildren<TextMesh>();
        plankText = texts[0];
        repairText = texts[1];

        repairText.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        repairCDRemaining = repairCD;
    }

    // Start is called before the first frame update
    void Start()
    {
        plankText.text = "Planks: " + plankCount + "/" + maxPlanks;

        

    }

    private void Update()
    {
        
    }

    public void AddPlank(float amount)
    {
                                                                        //PURELY COSMETIC  ---v
        if (plankCount == maxPlanks -1)
        {
            plankText.gameObject.SetActive(false);
            repairText.gameObject.SetActive(true);
        }
                                                                       // ---^
        if (!CheckIfCanAdd())
        {
                       

            return;
        }
        else
        {
            plankCount += amount;
            plankText.text = "Planks: " + plankCount + "/" + maxPlanks;
        }
        
    }

    public bool CheckIfCanAdd()
    {
        if (plankCount >= maxPlanks)
        {
            return false;
        }
        else return true;
    }

    public void Repair()
    {
        
        if (plankCount == maxPlanks)
        {
            print(repairCDRemaining);
            repairCDRemaining -= Time.deltaTime;
            if (repairCDRemaining <= 0)
            {
                // We've built the wall!
                if (wallType == Item.far)
                {
                    GameObject wallGO = (GameObject)Instantiate(wallFarPrefab, transform.position + wallSpawnOffset, transform.rotation);
                    Destroy(gameObject);
                }
                else if(wallType == Item.near)
                {
                    GameObject wallGO = (GameObject)Instantiate(wallNearPrefab, transform.position + wallSpawnOffset, transform.rotation);
                    Destroy(gameObject);
                } else if(wallType == Item.farWindow)
                {
                    GameObject wallGO = (GameObject)Instantiate(wallFarWindowPrefab, transform.position + wallSpawnOffset, transform.rotation);
                    Destroy(gameObject);
                }
                else if (wallType == Item.nearWindow)
                {
                    GameObject wallGO = (GameObject)Instantiate(wallNearWindowPrefab, transform.position + wallSpawnOffset, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
        else return;
    }

    void OnMouseDown()
    {
        AddPlank(1);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Repair();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && plankCount == maxPlanks)
        {
            transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
