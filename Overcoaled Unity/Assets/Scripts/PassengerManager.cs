using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassengerManager : MonoBehaviour
{
    [SerializeField] private float passengerY;
    [SerializeField] private int rowLength;
    [SerializeField] private int rowAmount;
    [SerializeField] private GameObject passenger;
    public List<GameObject> passengers = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI passengersAmountUI;
    // Start is called before the first frame update
    void Start()
    {/*
        Vector3 topLeftPassenger = transform.position;
        topLeftPassenger.y = passengerY;
        for (int a = 0; a < rowAmount; a++)
        {
            topLeftPassenger.z += 1;
            for (int l = 0; l < rowLength; l++)
            {
                topLeftPassenger.x += 1;
                passengers.Add(Instantiate(passenger, topLeftPassenger, Quaternion.identity));
                passengers[passengers.Count - 1].GetComponent<Renderer>().material.color = Random.ColorHSV();
                passengers[passengers.Count - 1].GetComponent<Passenger>().passengerManager = this;
            }
            topLeftPassenger.x = transform.position.x;
        }*/

        foreach(GameObject passenger in passengers)
        {
            passenger.GetComponent<Passenger>().passengerManager = this;
        }

        GameManager.GM.passengerCount = passengers.Count;
    }

    public void PassengerDead(GameObject passenger)
    {
        passengers.Remove(passenger);
        GameManager.GM.passengerCount = passengers.Count;
        passengersAmountUI.text = "x" + passengers.Count.ToString() + "/8";
    }


}
