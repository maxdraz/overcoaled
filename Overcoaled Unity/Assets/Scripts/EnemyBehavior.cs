using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public WallManager wallManager;
    public PassengerManager passengerManager;
    private Vector3 destination;
    [SerializeField] private bool arrived;
    [SerializeField] private float moveToPointSpeed;
    [SerializeField] private float fireRateDelay;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private GameObject enemyBullet;
    private EnemyGun gun;

    private enum carriage { back, middle, front};
    private carriage target;

    [SerializeField] private Transform shootTarget;

    private List<Player> players = new List<Player>();

    public void SetDestination(float setX, float setY, float setZ)
    {
        destination = new Vector3(setX, setY, setZ);
        SetTarget();
        gun = GetComponentInChildren<EnemyGun>();
        gun.rotateSpeed = rotateSpeed;
        gun.shootDelay = fireRateDelay;
        gun.bullet = enemyBullet;
    }

    public void SetTarget()
    {
        if (destination.x < 3)
        {
            target = carriage.back;
        }
        else if (destination.x < 12)
        {
            target = carriage.middle;
        }
        else
        {
            target = carriage.front;
        }
        players = GameManager.GM.players;

    }

    private int targetDistanceMax(carriage targetCarriage)
    {
        if (targetCarriage == carriage.front)
        {
            return 20;
        }
        else if (targetCarriage == carriage.middle)
        {
            return 12;
        }
        else
        {
            return 3;
        }
    }

    private int targetDistanceMin(carriage targetCarriage)
    {
        if (targetCarriage == carriage.front)
        {
            return 12;
        }
        else if (targetCarriage == carriage.middle)
        {
            return 3;
        }
        else
        {
            return -15;
        }
    }

    private int targetAreaToIndex(carriage targetCarriage)
    {
        if (targetCarriage == carriage.front)
        {
            return 1;
        }
        else if (targetCarriage == carriage.middle)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private void Update()
    {
        if (!arrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveToPointSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                arrived = true;
            }
        }
        else if (arrived)
        {
            shootTarget = null;

            foreach (WallClass wall in wallManager.Walls)
            {
                if (wall.wall != null)
                if (wall.position == targetAreaToIndex(target))
                {
                    if (shootTarget != null)
                    {
                        if (wall.wall != null)
                        if (Vector3.Distance(wall.wall.transform.position, transform.position)
                            < Vector3.Distance(shootTarget.position, transform.position))
                        {
                            if (wall.wall != null)
                            shootTarget = wall.wall.transform;
                        }
                    }
                    else
                    {
                        if (wall.wall != null)
                        shootTarget = wall.wall.transform;
                    }
                }
            }

            
            if (target == carriage.back)
            {
                print(Random.Range(0, passengerManager.passengers.Count));
                shootTarget = passengerManager.passengers[Random.Range(0, passengerManager.passengers.Count)].transform;
            }

            bool playerPriority = false;
            foreach (Player player in players)
            {
                if (player.playerObject.transform.position.x < targetDistanceMax(target)
                    && player.playerObject.transform.position.x > targetDistanceMin(target))
                {
                    
                    if (shootTarget != null)
                    {
                        if (playerPriority)
                        {
                            if (Vector3.Distance(player.playerObject.transform.position, transform.position)
                                < Vector3.Distance(shootTarget.position, transform.position))
                            {
                                shootTarget = player.playerObject.transform;
                                playerPriority = true;
                            }
                        }
                        else
                        {
                            shootTarget = player.playerObject.transform;
                            playerPriority = true;
                        }
                    }
                    else
                    {
                        shootTarget = player.playerObject.transform;
                        playerPriority = true;
                    }
                }
            }

            if (shootTarget != null)
            {
                gun.AimAndShoot(shootTarget);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
