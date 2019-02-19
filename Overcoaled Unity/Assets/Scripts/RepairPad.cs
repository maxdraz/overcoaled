using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPad : MonoBehaviour
{
    private TextMesh plankText;
    public bool isFar;
    [SerializeField]
    private float plankCount, maxPlanks;
    [SerializeField]
    private GameObject wallFarPrefab;
    [SerializeField]
    private GameObject wallNearPrefab;
    [SerializeField]
    private float repairCD = 4f;
    [SerializeField]
    private float repairCDRemaining;


    private void Awake()
    {
        plankText = GetComponentInChildren<TextMesh>();

    }

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        plankText.text = "Planks: " + plankCount + "/" + maxPlanks;
        

    }

   public void AddPlank(float amount)
    {
        if (plankCount > maxPlanks)
        {
            Debug.Log("Can't add any more planks");
            return;
        }
        else
        {
            plankCount += amount;
        }
        
    }

    void Repair()
    {
        if(plankCount == maxPlanks)
        {
            repairCDRemaining -= Time.deltaTime;
            if(repairCDRemaining <= 0)
            {
                // We've built the wall!
                if (isFar)
                {
                    GameObject wallGO = (GameObject)Instantiate(wallFarPrefab, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else
                {
                    GameObject wallGO = (GameObject)Instantiate(wallNearPrefab, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
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
}
