using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{

    [SerializeField] private int fullTravelLength;
    [SerializeField] private int fullTimeLength;
    [SerializeField] public float travelDistance = 0;
    [SerializeField] private float timeSinceStart = 0;
    private bool travelBegun;
    private int currentSpeed;
    // Update is called once per frame
    void Update()
    {
        if (travelBegun)
        {
            timeSinceStart += Time.deltaTime;
            travelDistance += currentSpeed * Time.deltaTime;
            if (travelDistance >= fullTravelLength)
            {
                //End game
            }
            else if (timeSinceStart >= fullTimeLength)
            {
                //Game over
            }
        }
    }

    public void StartTimer()
    {
        travelBegun = true;
    }

    public void AddDistance(int speed)
    {
        currentSpeed = speed;
    }
}
