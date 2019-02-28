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

    private float throwDynamiteTimer;
    [SerializeField] public SpawnDynamite spawnDynamite;
    private bool dynamiteThrown = false;

    private int enemyHealth = 3;

    private MultipleTargetCamera cam;
    private int[] randomSounds = new int[] { 5, 6, 7, 8, 9 };





    private void Awake()
    {
        cam = GameObject.FindObjectOfType<MultipleTargetCamera>();
    }

    private void OnEnable()
    {
        cam.AddTarget(transform);

        StartCoroutine(PlayRandomSound());
       
    }

    public void SetDestination(float setX, float setY, float setZ)
    {
        destination = new Vector3(setX, setY, setZ);
        SetTarget();
        gun = GetComponentInChildren<EnemyGun>();
        gun.rotateSpeed = rotateSpeed;
        gun.shootDelay = fireRateDelay;
        gun.bullet = enemyBullet;

        throwDynamiteTimer = Random.Range(20.0f, 30.0f);
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


            //if (target == carriage.back)
            //{
            //    if (passengerManager.passengers.Count > 0)
            //    {
            //        int passengerNumber = Random.Range(0, passengerManager.passengers.Count);
            //        if (passengerManager.passengers[passengerNumber])
            //        {
            //            shootTarget = passengerManager.passengers[passengerNumber].transform;
            //        }
            //    }
            //}

            bool cargoPriority = false;
            foreach (GameObject passenger in passengerManager.passengers)
            {
                if (passenger)
                {
                    if (passenger.transform.position.x < targetDistanceMax(target)
                        && passenger.transform.position.x > targetDistanceMin(target))
                    {

                        if (shootTarget != null)
                        {
                            if (cargoPriority)
                            {
                                if (Vector3.Distance(passenger.transform.position, transform.position)
                                    < Vector3.Distance(shootTarget.position, transform.position))
                                {
                                    shootTarget = passenger.transform;
                                    cargoPriority = true;
                                }
                            }
                            else
                            {
                                shootTarget = passenger.transform;
                                cargoPriority = true;
                            }
                        }
                        else
                        {
                            shootTarget = passenger.transform;
                            cargoPriority = true;
                        }
                    }
                }
            }



            bool playerPriority = false;
            foreach (Player player in players)
            {
                if (player.playerObject)
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
            }

            if (shootTarget != null)
            {
                gun.AimAndShoot(shootTarget);
            }

        }

        throwDynamiteTimer -= Time.deltaTime;
        if (throwDynamiteTimer <= 0 && !dynamiteThrown)
        {
            spawnDynamite.AddDynamite();
            dynamiteThrown = true;
        }
    }

    public void TakeDamage()
    {
        enemyHealth -= 1;
        if (enemyHealth <= 0)
        {
            cam.RemoveTarget(transform);
            Destroy(gameObject);
            
        }
    }

    IEnumerator PlayRandomSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 10f));

            AudioManager.SharedInstance.PlayClip(randomSounds[Random.Range(0, randomSounds.Length - 1)]);
        }
    }
    
}
