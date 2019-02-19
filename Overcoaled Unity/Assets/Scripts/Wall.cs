using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float health = 3f;
    private TextMesh healthText;

    private void Awake()
    {
        healthText = GetComponentInChildren<TextMesh>();
        healthText.text = "HP: " + health;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

   

}
