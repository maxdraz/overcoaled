using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectMove : MonoBehaviour
{
    private Rigidbody rb;
    public bool isOnPlayer;
    [SerializeField] private float level1Force;
    [SerializeField] private float level2Force;
    [SerializeField] private float level3Force;
    public float forceScale = 8f;
    public bool grounded = false;
    public int groundLayer;
    public int noPlayerCollisionLayer;
    public GameObject groundHitPS;
    [SerializeField] private GameObject bloodParticle;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        //rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        StartCoroutine(changePhysicsLayer());
        grounded = false;
    }

    private void Update()
    {
        if(gameObject.tag == "Passenger" && grounded)
        {
            
            this.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if(isOnPlayer && collision.gameObject.layer == groundLayer && !grounded)
        {
            //ps
            grounded = true;
            GameObject ps = (GameObject)Instantiate(groundHitPS, transform.position, Quaternion.identity);
            gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.layer = 0;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            this.enabled = false;

        }

        else if(collision.gameObject.layer == groundLayer && !grounded)
        {
            
            grounded = true;
            GameObject ps = (GameObject)Instantiate(groundHitPS, transform.position, Quaternion.identity);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
           
            other.transform.parent.GetComponent<EnemyBehavior>().TakeDamage(1);
            GameObject ps = (GameObject)Instantiate(bloodParticle, gameObject.transform.position, Quaternion.identity);
            print("should have destroyed");
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        rb.AddForce(transform.forward * forceScale, ForceMode.Impulse);

        if(gameObject.tag == "Gun")
        {
            rb.AddTorque(transform.right * forceScale, ForceMode.Impulse);
        }
    }

    public void setForceLevel(int level)
    {
        Mathf.Clamp(level, 1, 3);
        if(level == 1)
        {
            forceScale = level1Force;
        }
        
        if(level == 2)
        {
            forceScale = level2Force;
        }

        if(level == 3)
        {
            forceScale = level3Force;
        }
    }

    IEnumerator changePhysicsLayer()
    {
        gameObject.layer = noPlayerCollisionLayer;
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = 0;
    }

    
}
