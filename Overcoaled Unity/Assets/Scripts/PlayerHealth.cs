using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float respawnCD;
    private float maxHealth;
     public Transform respawn;
    Rigidbody rb;
    PlayerMove move;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = GetComponent<PlayerMove>();
        respawn = GameObject.Find("Respawn").transform;
    }

    
    IEnumerator Respawn(float t)
    {
        transform.position = respawn.position;
        transform.rotation = respawn.rotation;
        health = maxHealth;

        rb.isKinematic = true;
        move.enabled = false;
        yield return new WaitForSeconds(t);
        rb.isKinematic = false;
        move.enabled = true;


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
