using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int controllerNumber;
    public int health;
    public float speed;
    public GameObject playerObject;

    public Player(int controllerNum, int HP, float moveSpeed)
    {
        controllerNumber = controllerNum;
        health = HP;
        speed = moveSpeed;
    }
}
