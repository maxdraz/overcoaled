using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
