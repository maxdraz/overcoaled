using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnLocation;
    public int playerNumber;
    public int ammo;
    public int maxAmmo;
    private bool canShoot = true;
    [SerializeField] private float shootDelayTime;

    private void Start()
    {
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("joystick " + playerNumber + " RTrigger") == 1 && ammo > 0 && canShoot)
        {
            ammo--;
            canShoot = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        print(Input.GetAxisRaw("joystick " + playerNumber + " RTrigger"));
        GameObject projectile = Instantiate(bullet, spawnLocation.position, spawnLocation.rotation);
        
        Invoke("shotDelay", shootDelayTime);
    }

    private void shotDelay()
    {
        canShoot = true;
    }

    public void Reload()
    {
        ammo = maxAmmo;
    }
}
