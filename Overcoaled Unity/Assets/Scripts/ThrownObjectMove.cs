﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectMove : MonoBehaviour
{
    private Rigidbody rb;
    public bool isOnPlayer;
    [SerializeField] private float level1Force;
    [SerializeField] private float level2Force;
    [SerializeField] private float level3Force;
    public float forceScale = 8f;
    [SerializeField] private bool grounded = false;
    public int groundLayer;
    public int noPlayerCollisionLayer;
    public GameObject groundHitPS;
    [SerializeField] private GameObject bloodParticle;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        //rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        StartCoroutine(changePhysicsLayer());
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage();
            GameObject ps = (GameObject)Instantiate(bloodParticle, gameObject.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if(isOnPlayer && collision.gameObject.layer == groundLayer && !grounded)
        {
            //ps
            grounded = true;
            GameObject ps = (GameObject)Instantiate(groundHitPS, transform.position, Quaternion.identity);
            gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.layer = 0;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            this.enabled = false;
            print("called");
        }

        else if(collision.gameObject.layer == groundLayer && !grounded)
        {
            
            grounded = true;
            GameObject ps = (GameObject)Instantiate(groundHitPS, transform.position, Quaternion.identity);
        }

        
    }

    public void Move()
    {
        rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);

        if(gameObject.tag == "Gun")
        {
            rb.AddTorque(transform.right * forceScale, ForceMode.Impulse);
        }
    }

    public void setForceLevel(int level)
    {
        Mathf.Clamp(level, 1, 3);
        if(level == 1)
        {
            forceScale = level1Force;
        }
        
        if(level == 2)
        {
            forceScale = level2Force;
        }

        if(level == 3)
        {
            forceScale = level3Force;
        }
    }

    IEnumerator changePhysicsLayer()
    {
        gameObject.layer = noPlayerCollisionLayer;
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 0;
    }

    
}
