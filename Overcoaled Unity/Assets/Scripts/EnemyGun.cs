using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [HideInInspector] public float shootDelay;
    [HideInInspector] public float rotateSpeed;
    [HideInInspector] public GameObject bullet;
    private bool canShoot = true;
    

    public void AimAndShoot(Transform target)
    {
        Vector3 targetDir = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = rotateSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        if (canShoot)
        {
            canShoot = false;
            Invoke("Shoot", shootDelay);
        }
        
    }

    private void Shoot()
    {
        canShoot = true;

        Instantiate(bullet, transform.GetChild(0).transform.GetChild(0).transform.position, transform.rotation);
    }


}
