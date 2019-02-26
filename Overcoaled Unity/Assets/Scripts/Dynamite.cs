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
            if(transform.parent != null)
            {
                transform.root.GetComponent<PlayerInteraction>().Drop();
                transform.parent = null;
            }
            
            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.tag = "Death";
            GetComponent<Animator>().enabled = true;
            
            Destroy(gameObject, 1);
        }
    }
}
