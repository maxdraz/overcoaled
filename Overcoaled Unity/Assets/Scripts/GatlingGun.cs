﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    [SerializeField] private bool southFacing;

    private int playerNumber;
    private bool inUse;
    [SerializeField] private float shootDelayTime;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private int ammo;
    [SerializeField] private int ammoPerLoad;
    [SerializeField] private int shotsPerCooldown;
    [SerializeField] private float cooldownDuration;
    [SerializeField] private Transform spawnLocation;
    private bool canShoot = true;
    private bool coolingDown = false;

    public SpriteRenderer aButton;

    private Color selfColor;
    private Renderer renderer;

    private void Start()
    {
        
        renderer = GetComponentInChildren<Renderer>();
        selfColor = renderer.material.color;
    }

    public void SetPlayer(int playerNum)
    {
        if (!inUse)
        {
            playerNumber = playerNum;
            inUse = true;
        }
    }

    public void ExitGun()
    {
        inUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            Vector3 direction;
            if ((Input.GetAxis("joystick R " + playerNumber + " horizontal") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " horizontal") < -0.2)
                || (Input.GetAxis("joystick R " + playerNumber + " vertical") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " vertical") < -0.2))
            {
                float dirX = Input.GetAxis("joystick R " + playerNumber + " horizontal");
                float dirY = Input.GetAxis("joystick R " + playerNumber + " vertical");

                if (southFacing)
                {
                    print(dirX + ", " + dirY);
                    dirX = Mathf.Clamp(dirX, -1f, 1f);
                    dirY = Mathf.Clamp(dirY, 0.2f, 1f);
                }
                else
                {
                    print(dirX + ", " + dirY);
                    dirX = Mathf.Clamp(dirX, -1f, 1f);
                    dirY = Mathf.Clamp(dirY, -0.2f, -1f);
                }

                direction = new Vector3(dirX, 0.0f, -dirY);

                float step = rotateSpeed * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }

            if (Input.GetAxisRaw("joystick " + playerNumber + " RTrigger") == 1)
            {
                GetComponent<Animator>().enabled = true;
                if (ammo > 0 && canShoot)
                {
                    ammo--;
                    float ammoPercent = (100.0f - ammo) / 100.0f;
                    print(ammoPercent);
                    renderer.material.color = Color.Lerp(selfColor, Color.red, ammoPercent);
                    canShoot = false;
                    Shoot();
                }
            }
            else
            {
                GetComponent<Animator>().enabled = false;
            }

            if (ammo == 0 && !coolingDown)
            {
                Invoke("CoolDown", cooldownDuration);
                coolingDown = true;
            }
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

    private void CoolDown()
    {
        ammo = 50;
        renderer.material.color = Color.Lerp(selfColor, Color.red, (100 - ammo) / 100);
        coolingDown = false;
    }

    public void LoadAmmo()
    {
        ammo += ammoPerLoad;
    }

    
}
