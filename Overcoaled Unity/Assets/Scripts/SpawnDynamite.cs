using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDynamite : MonoBehaviour
{
    [SerializeField] private GameObject dynamite;
    [SerializeField] private float waitTillDynamiteDrops;
    [SerializeField] private Vector3 aboveTrainTopLeft;
    [SerializeField] private Vector3 aboveTrainBottomRight;
    private Queue<GameObject> DynamiteQueue = new Queue<GameObject>();
    public void AddDynamite()
    {
        DynamiteQueue.Enqueue(dynamite);
        Invoke("DropDynamite", waitTillDynamiteDrops);
    }

    private void DropDynamite()
    {
        float x = Random.Range(aboveTrainTopLeft.x, aboveTrainBottomRight.x);
        float z = Random.Range(aboveTrainTopLeft.z, aboveTrainBottomRight.z);
        float y = aboveTrainTopLeft.y;
        Instantiate(DynamiteQueue.Dequeue(), new Vector3(x, y, z), dynamite.transform.rotation);
    }

    
}
