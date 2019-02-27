using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public enum Item { far, farWindow, near, nearWindow }
    public Item typeOfWall;
    public float health = 3f;
    [SerializeField] private GameObject repairPadPrefab;
    
    
    private Vector3 repairPadOffset = new Vector3(0, -0.5f, 0);
    //private TextMesh healthText;

    private void Awake()
    {
        //healthText = GetComponentInChildren<TextMesh>();
        //healthText.text = "HP: " + health;
    }

    public void TakeDamage(float dmg)
    {
      

        health -= dmg;
       // healthText.text = "HP: " + health;

        if (health <= 0)
        {
            GameObject repairPadGO = (GameObject)Instantiate(repairPadPrefab, transform.position + repairPadOffset, transform.rotation);
            //repairPadGO.GetComponent<RepairPad>().wallType =(RepairPad.Item) typeOfWall;
            repairPadGO.GetComponent<RepairPad>().wallObj = gameObject;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }


    }

   

   

}
