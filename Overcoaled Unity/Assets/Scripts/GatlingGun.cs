using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    private int playerNumber;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int ammo;
    [SerializeField] private Transform spawnLocation;
    private bool canShoot = true;

    public void SetPlayer(int playerNum)
    {
        playerNumber = playerNum;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        if ((Input.GetAxis("joystick R " + playerNumber + " horizontal") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " horizontal") < -0.2)
            || (Input.GetAxis("joystick R " + playerNumber + " vertical") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " vertical") < -0.2))
        {
            float dirX = Input.GetAxis("joystick R " + playerNumber + " horizontal");
            float dirY = Input.GetAxis("joystick R " + playerNumber + " vertical");
            dirX = Mathf.Clamp(dirX, -0.6f, 0.6f);
            dirY = Mathf.Clamp(dirY, 0.7f, 1f);
            print(dirX + ", " + dirY);
            direction = new Vector3(dirX, 0.0f, -dirY);
            
            float step = rotateSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        if (Input.GetAxisRaw("joystick " + playerNumber + " RTrigger") == 1 && ammo > 0 && canShoot)
        {
            ammo--;
            canShoot = false;
            Shoot();
        }
    }

    private void Shoot()
    {

        GameObject projectile = Instantiate(bullet, spawnLocation.position, spawnLocation.rotation);

        Invoke("shotDelay", shootDelayTime);
    }

    private void shotDelay()
    {
        canShoot = true;
    }
}
