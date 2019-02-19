using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health = 3f;    

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    // JUST FOR TESTING
    private void OnMouseDown()
    {
        Debug.Log("hit!");
        TakeDamage(1f);
    }
}
