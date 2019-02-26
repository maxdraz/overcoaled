using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public List<Player> players = new List<Player>();
    [SerializeField] private int playerHealth = 0;
    [SerializeField] private float playerSpeed = 0;
    [SerializeField] private float playerSlowSpeed = 0;
    [Tooltip("0 = player 1, etc")] [SerializeField] private Vector3[] playerSpawnLocations;
    [SerializeField] private GameObject playerCharacter;
    [Tooltip("0 = player 1, etc")] [SerializeField] private Color[] playerColours;

    [SerializeField] TravelManager travelManager;

     public int passengerCount;

    [SerializeField] private UIManager uiManager;

    //Awake is always called before any Start functions

    void Awake()
    {
        //Check if instance already exists
        if (GM == null)
        {
            //if not, set instance to this
            GM = this;
        }
        //If instance already exists and it's not this:
        else if (GM != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);

    }

    public void AddPlayer(int playerNum)
    {

        players.Add(new Player(playerNum, playerHealth, playerSpeed));
        players[players.Count - 1].playerObject = (GameObject)Instantiate(playerCharacter, playerSpawnLocations[players.Count - 1], Quaternion.identity);
        players[players.Count - 1].playerObject.GetComponent<PlayerMove>().playerNumber = playerNum;
        players[players.Count - 1].playerObject.GetComponent<PlayerMove>().SetSpeed(playerSpeed);
        players[players.Count - 1].playerObject.GetComponent<PlayerMove>().slowMoveSpeed = playerSlowSpeed;
        players[players.Count - 1].playerObject.GetComponent<PlayerMove>().normalMoveSpeed = playerSpeed;
        players[players.Count - 1].playerObject.GetComponent<PlayerShoot>().playerNumber = playerNum;
        players[players.Count - 1].playerObject.GetComponent<PlayerInteraction>().playerNumber = playerNum;



        //////////////move this
        travelManager.StartTimer();
    }

    public void EndGame(int timeLeft)
    {
        uiManager.SetUI(passengerCount, timeLeft);
    }

}
