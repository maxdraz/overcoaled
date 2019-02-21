using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WallClass
{
    public GameObject wall;
    public int position; // 1 = front, 2 = middle, 3 = back

    public WallClass(GameObject newWall, int newPos)
    {
        wall = newWall;
        position = newPos;
    }
}
