using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovePhysics : MonoBehaviour
{
    private TravelManager travelManager;
    [SerializeField] private int groundSpeedMultiplier;
    private void Start()
    {
        travelManager = GetComponentInParent<TravelManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Rigidbody>())
        {
            float speed = travelManager.offset * groundSpeedMultiplier * Time.deltaTime;
            other.transform.Translate(Vector3.left * speed, Space.World);
        }
    }
}
