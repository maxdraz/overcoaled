﻿using System.Collections;
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
        GetComponent<PlayerInteraction>().Drop();
        transform.GetComponentInChildren<ParticleSystem>().Stop();
        transform.Find("Player Character").gameObject.SetActive(false);
        transform.position = respawn.position;
        transform.rotation = respawn.rotation;
        health = maxHealth;


        rb.isKinematic = true;
        move.enabled = false;
        yield return new WaitForSeconds(t);

        transform.Find("Player Character").gameObject.SetActive(true);
        transform.GetComponentInChildren<ParticleSystem>().Play();
        rb.isKinematic = false;
        move.enabled = true;
        health = maxHealth;
    }

    public void SetHealth(float h)
    {
        health = h;
        maxHealth = h;
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            // StartCoroutine(Respawn(respawnCD));
            DownPlayer();
        }
    }

    private void KillPlayer()
    {
        print("touched death");
        StartCoroutine(Respawn(respawnCD));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Invoke("KillPlayer", 2);
        }
    }

    private void DownPlayer()
    {
        gameObject.tag = "PlayerDown";
        GetComponent<PlayerMove>().enabled = false;
        GetComponent<PlayerInteraction>().Drop();
        GetComponent<PlayerInteraction>().enabled = false;
    }

    public void RevivePlayer()
    {
        gameObject.tag = "Player";
        SetHealth(maxHealth);
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerInteraction>().enabled = true;
    }
}
