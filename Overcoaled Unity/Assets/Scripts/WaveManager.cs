using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyWave[] enemyWaves;
    private TravelManager travelManager;
    [SerializeField] private GameObject enemy;
    [SerializeField] private WallManager wallManager;
    [SerializeField] private PassengerManager passengerManager;
    [SerializeField] private SpawnDynamite dynamiteSpawner;
    [Space(10)]
    [Header("Upper Spawn 1")]
    [SerializeField] private Vector2 topLeftOfUpperSpawn;
    [SerializeField] private Vector2 bottomRighttOfUpperSpawn;
    [Header("Upper Spawn 2")]
    [SerializeField] private Vector2 topLeftOfUpperSpawn2;
    [SerializeField] private Vector2 bottomRighttOfUpperSpawn2;
    [Space(5)]
    [Header("Lower Spawn 1")]
    [SerializeField] private Vector2 topLeftOfLowerSpawn;
    [SerializeField] private Vector2 bottomRightOfLowerSpawn;
    [Header("Lower Spawn 2")]
    [SerializeField] private Vector2 topLeftOfLowerSpawn2;
    [SerializeField] private Vector2 bottomRightOfLowerSpawn2;
    [Space(10)]
    [Header("Upper Destination")]
    [SerializeField] private Vector2 topLeftOfUpperDestination;
    [SerializeField] private Vector2 bottomRightOfUpperDestination;
    [Header("Lower Destination")]
    [SerializeField] private Vector2 topLeftOfLowerDestination;
    [SerializeField] private Vector2 bottomRightOfLowerDestination;
    [SerializeField] private float vectorY = 0;

    private void Start()
    {
        travelManager = GetComponent<TravelManager>();
    }

    private void Update()
    {
        foreach (EnemyWave wave in enemyWaves)
        {
            if ((wave.enterDistance <= travelManager.travelDistance+3 && wave.enterDistance >= travelManager.travelDistance-3) && !wave.activated)
            {
                EnterWave(wave);
                wave.activated = true;
            }
        }
    }

    private void EnterWave(EnemyWave wave)
    {
        wave.activated = false;
        for (int i = 0; i < wave.amount; i++)
        {
            int x = 0;
            int z = 0;
            int spawn = Random.Range(0, 2);
            if (i % 2 == 0)
            {
                if (spawn == 0)
                {
                    x = (int)Random.Range(topLeftOfUpperSpawn.x, bottomRighttOfUpperSpawn.x);
                    z = (int)Random.Range(topLeftOfUpperSpawn.y, bottomRighttOfUpperSpawn.y);
                }
                else
                {
                    x = (int)Random.Range(topLeftOfUpperSpawn2.x, bottomRighttOfUpperSpawn2.x);
                    z = (int)Random.Range(topLeftOfUpperSpawn2.y, bottomRighttOfUpperSpawn2.y);
                }
            }
            else
            {
                if (spawn == 0)
                {
                    x = (int)Random.Range(topLeftOfLowerSpawn.x, bottomRightOfLowerSpawn.x);
                    z = (int)Random.Range(topLeftOfLowerSpawn.y, bottomRightOfLowerSpawn.y);
                }
                else
                {
                    x = (int)Random.Range(topLeftOfLowerSpawn2.x, bottomRightOfLowerSpawn2.x);
                    z = (int)Random.Range(topLeftOfLowerSpawn2.y, bottomRightOfLowerSpawn2.y);
                }
            }
            wave.enemyBody = Instantiate(enemy, new Vector3(x, vectorY, z), Quaternion.identity);

            if (i % 2 == 0)
            {
                x = (int)Random.Range(topLeftOfUpperDestination.x, bottomRightOfUpperDestination.x);
                z = (int)Random.Range(topLeftOfUpperDestination.y, bottomRightOfUpperDestination.y);
            }
            else
            {
                x = (int)Random.Range(topLeftOfLowerDestination.x, bottomRightOfLowerDestination.x);
                z = (int)Random.Range(topLeftOfLowerDestination.y, bottomRightOfLowerDestination.y);
            }
            wave.enemyBody.GetComponent<EnemyBehavior>().SetDestination(x, vectorY, z);
            wave.enemyBody.GetComponent<EnemyBehavior>().wallManager = wallManager;
            wave.enemyBody.GetComponent<EnemyBehavior>().passengerManager = passengerManager;
            wave.enemyBody.GetComponent<EnemyBehavior>().spawnDynamite = dynamiteSpawner;
        }
    }

}
