using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int wallLayerIndex = 10;
    
    // Update is called once per frame
    void  FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

   
}
