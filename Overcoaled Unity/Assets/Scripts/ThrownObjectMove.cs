using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectMove : MonoBehaviour
{
    private Rigidbody rb;
    public float forceScale = 8f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);
    }

    
  
}
