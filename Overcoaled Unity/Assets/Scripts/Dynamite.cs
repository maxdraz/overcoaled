using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private float timer;


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GetComponent<Animator>().enabled = true;
            Destroy(gameObject, 1);
        }
    }
}
