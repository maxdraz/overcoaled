using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isFar;
    public float health = 3f;
    [SerializeField] private GameObject repairPadPrefab;
    [SerializeField] private GameObject bulletDeathParticle;
    
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
            repairPadGO.GetComponent<RepairPad>().isFar = isFar;
            Destroy(gameObject);
        }


    }
                                                                                   // REMOVE AFTER TESTING
    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            print("colliding");
            //GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.GetContact(0).point, Quaternion.identity);
            Destroy(collision.gameObject);
            GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.gameObject.transform.position, Quaternion.identity);
        }
    }

}
