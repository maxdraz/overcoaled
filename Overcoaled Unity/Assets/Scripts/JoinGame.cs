using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class JoinGame : MonoBehaviour
{

    List<int> playersJoined = new List<int>();

    // Update is called once per frame
    void Update()
    {
        int controllerAmount = Input.GetJoystickNames().Length;
        
        for(int i = 0; i < controllerAmount; i++)
        {
            if (Input.GetButtonDown("joystick " + (int)(i + 1) + " start"))
            {
                bool alreadyJoined = false;
                foreach (int p in playersJoined)
                {
                    if (p == (i + 1))
                    {
                        alreadyJoined = true;
                    }
                }

                if (alreadyJoined)
                {
                    continue;
                }

                GameManager.GM.AddPlayer(i + 1);
                playersJoined.Add(i + 1);
            }
        }
    }
}
