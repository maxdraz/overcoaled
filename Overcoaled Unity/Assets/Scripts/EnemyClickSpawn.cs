using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClickSpawn : MonoBehaviour
{
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseposition = Input.mousePosition;
        mouseposition.z = 5;
        mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
        mouseposition.y = 2f;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(enemy, mouseposition, Quaternion.identity);
        }
    }
}
