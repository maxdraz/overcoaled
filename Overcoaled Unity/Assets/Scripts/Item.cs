using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private bool grounded = false;
    public int groundLayerIndex;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == groundLayerIndex)
        {
            
            grounded = true;
        }
    }
}
