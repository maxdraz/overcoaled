using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletDeathParticle;
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private bool isPlayerBullet;
    
    // Update is called once per frame
    void  FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerBullet && other.tag == "Enemy")
        {
            GameObject ps = (GameObject)Instantiate(bloodParticle, gameObject.transform.position, Quaternion.identity);
            other.gameObject.transform.parent.GetComponent<EnemyBehavior>().TakeDamage(1);
            Destroy(gameObject);
        }

        if(!isPlayerBullet && other.tag == "Player")
        {
            GameObject ps = (GameObject)Instantiate(bloodParticle, gameObject.transform.position, Quaternion.identity);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(gameObject);
        }


        if (gameObject.tag == "Enemy Bullet" && other.gameObject.tag == "Wall")
        {
            if(other.gameObject.GetComponent<Wall>())
            other.gameObject.GetComponent<Wall>().TakeDamage(2);
        }
        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "MidWall")
        {

            //GameObject ps = (GameObject)Instantiate(bulletDeathParticle, collision.GetContact(0).point, Quaternion.identity);            

            GameObject ps = (GameObject)Instantiate(bulletDeathParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        
            
        }
    }


