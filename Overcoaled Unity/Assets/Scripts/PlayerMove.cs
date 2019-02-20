﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerNumber;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        float posX = Input.GetAxis("joystick L " + playerNumber + " horizontal") * moveSpeed;
        float posY = Input.GetAxis("joystick L " + playerNumber + " vertical") * -moveSpeed;

        if ((Input.GetAxis("joystick R " + playerNumber + " horizontal") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " horizontal") < -0.2)
            || (Input.GetAxis("joystick R " + playerNumber + " vertical") > 0.2 || Input.GetAxis("joystick R " + playerNumber + " vertical") < -0.2))
        {
            float dirX = Input.GetAxis("joystick R " + playerNumber + " horizontal");
            float dirY = Input.GetAxis("joystick R " + playerNumber + " vertical");

            Vector3 direction = new Vector3(dirX, 0.0f, -dirY);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.Translate(Vector3.right * posX * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * posY * Time.deltaTime, Space.World);
        
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
