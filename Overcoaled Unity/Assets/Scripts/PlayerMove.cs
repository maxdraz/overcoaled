using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int playerNumber;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        float posX = Input.GetAxis("joystick " + playerNumber + " horizontal") * moveSpeed;
        float posY = Input.GetAxis("joystick " + playerNumber + " vertical") * -moveSpeed;

        if ((Input.GetAxis("joystick " + playerNumber + " horizontal") > 0.2 || Input.GetAxis("joystick " + playerNumber + " horizontal") < -0.2)
            || (Input.GetAxis("joystick " + playerNumber + " vertical") > 0.2 || Input.GetAxis("joystick " + playerNumber + " vertical") < -0.2))
        {
            Vector3 direction = new Vector3(posX, 0.0f, posY);
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
