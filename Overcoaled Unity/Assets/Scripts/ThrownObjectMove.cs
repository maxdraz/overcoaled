﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectMove : MonoBehaviour
{
    private Rigidbody rb;
    public float forceScale = 8f;
    [SerializeField] private bool grounded = false;
    public int groundLayerIndex;
    public GameObject groundHitPS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayerIndex && !grounded)
        {
            print(collision.gameObject.name);
            grounded = true;
            GameObject ps = (GameObject)Instantiate(groundHitPS, transform.position, Quaternion.identity);
        }
    }

}
