using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    [SerializeField] private Vector3 topLeftPassenger;
    [SerializeField] private int rowLength;
    [SerializeField] private int rowAmount;
    [SerializeField] private GameObject passenger;
    public List<GameObject> passengers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int z = (int)topLeftPassenger.z;
        int x = (int)topLeftPassenger.x;
        for (int a = z; a > -(z+rowAmount); a--)
        {
            for (int l = x; l < x+rowLength; l++)
            {
                topLeftPassenger.x = l;
                passengers.Add(Instantiate(passenger, topLeftPassenger, Quaternion.identity));
                passengers[passengers.Count - 1].GetComponent<Renderer>().material.color = Random.ColorHSV();
            }
            topLeftPassenger.z = a;
        }
    }
}
