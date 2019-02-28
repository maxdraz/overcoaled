using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private GameObject explosion;
    private bool exploded;


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !exploded)
        {
            exploded = true;
            if (transform.parent != null)
            {
                transform.root.GetComponent<PlayerInteraction>().Drop();
                transform.parent = null;
            }

            GetComponent<Rigidbody>().isKinematic = false;
            gameObject.tag = "Death";
            GetComponent<Animator>().enabled = true;
            GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionObject, 1);
            Destroy(gameObject, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "Death" && collision.gameObject.tag == "Wall")
        {
            if (collision.gameObject.GetComponent<Wall>())
                collision.gameObject.GetComponent<Wall>().TakeDamage(4);
        }
        if (gameObject.tag == "Death" && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.parent.GetComponent<EnemyBehavior>().TakeDamage(3);
        }
    }
}
