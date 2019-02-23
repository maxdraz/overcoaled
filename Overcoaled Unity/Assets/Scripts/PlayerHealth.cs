using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float respawnCD;
    private float maxHealth;
     public Transform respawn;

    
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Respawn(float t)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(t);
        
        health = maxHealth;
        transform.position = respawn.position;
        transform.rotation = respawn.rotation;

    }

    public void SetHealth(float h)
    {
        health = h;
        maxHealth = h;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            StartCoroutine(Respawn(respawnCD));
        }
    }
}
