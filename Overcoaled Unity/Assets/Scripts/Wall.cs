using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum Item { far, farWindow, near, nearWindow }
    public Item typeOfWall;
    public float health = 3f;
    public float maxHealth = 3f;
    [SerializeField] private GameObject repairPadPrefab;
    [SerializeField] private Transform repairPadTransform;


    private Vector3 repairPadOffset = new Vector3(0, -0.5f, 0);
    //private TextMesh healthText;

    private void Awake()
    {
        repairPadTransform = transform.GetChild(transform.childCount - 1);
        //healthText = GetComponentInChildren<TextMesh>();
        //healthText.text = "HP: " + health;
    }

    private void OnEnable()
    {
        health = maxHealth;   
    }

    public void TakeDamage(float dmg)
    {
      

        health -= dmg;
       // healthText.text = "HP: " + health;

        if (health <= 0)
        {
            GameObject repairPadGO = (GameObject)Instantiate(repairPadPrefab, repairPadTransform.position, repairPadTransform.rotation);
            //repairPadGO.GetComponent<RepairPad>().wallType =(RepairPad.Item) typeOfWall;
            repairPadGO.GetComponent<RepairPad>().wallObj = gameObject;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }


    }

    

   

   

}
