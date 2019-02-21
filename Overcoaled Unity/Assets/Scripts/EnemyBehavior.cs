using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Vector3 destination;
    [SerializeField] private bool arrived;
    [SerializeField] private float moveToPointSpeed;
    [SerializeField] private float fireRateDelay;

    public void SetDestination(float setX, float setY, float setZ)
    {
        destination = new Vector3(setX, setY, setZ);
    }

    private void Update()
    {
        if (!arrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveToPointSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                arrived = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
