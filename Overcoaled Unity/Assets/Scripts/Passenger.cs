using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    [HideInInspector] public PassengerManager passengerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Bullet")
        {
            passengerManager.PassengerDead(gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            passengerManager.PassengerDead(gameObject);
            Destroy(gameObject, 1f);
        }
    }
}
