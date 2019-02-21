using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public List<WallClass> Walls = new List<WallClass>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            if (other.gameObject.transform.position.x < 3)
            {
                Walls.Add(new WallClass(other.gameObject, 3));
            }
            else if (other.gameObject.transform.position.x < 12)
            {
                Walls.Add(new WallClass(other.gameObject, 2));
            }
            else
            {
                Walls.Add(new WallClass(other.gameObject, 1));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            foreach(WallClass wall in Walls)
            {
                if (wall.wall == null)
                {
                    Walls.Remove(wall);
                }
            }
        }
    }
}
