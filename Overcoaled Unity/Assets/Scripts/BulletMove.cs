using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletDeathParticle;
    
    // Update is called once per frame
    void  FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            print("collided with bullet");
            //GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.GetContact(0).point, Quaternion.identity);            

            GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.GetContact(0).point, Quaternion.identity);
            Destroy(gameObject);
        }

        
            if (gameObject.tag == "Enemy Bullet" && collision.gameObject.tag == "Wall")
            {
                collision.gameObject.GetComponent<Wall>().TakeDamage(1);

                GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.GetContact(0).point, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }


