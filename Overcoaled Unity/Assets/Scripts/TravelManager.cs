using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelManager : MonoBehaviour
{

    [SerializeField] private int fullTravelLength;
    [SerializeField] private int fullTimeLength;
    [SerializeField] public float travelDistance = 0;
    [SerializeField] private float timeSinceStart = 0;
    private bool travelBegun;
    private int currentSpeed;
    private float offsetSpeed;
    public float offset;
    [SerializeField] private float lerpTime;
    [SerializeField] private int offsetMultiplier;
    [SerializeField] private float lerp = 0;
    private float totalOffset;
    private float offsetLerp;

    [SerializeField] private Renderer ground;

    [SerializeField] private Slider distanceUI;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] private TextMeshProUGUI distanceTravelled;

    private bool gameOver = false;
    private bool behindOnTimeBoost;

    public List<GameObject> rails;
    private Queue<GameObject> railsQueue;
    [SerializeField] private float railsSpeed;
    [SerializeField] private ParticleSystem smoke;

    [SerializeField] private GameObject cactus, rock;
    [SerializeField] private Transform[] backgroundObjectSpawnPosition;
    private Queue<GameObject> backgroundObjectsQueue = new Queue<GameObject>();
    private int backgroundObjectMadeForDistance;

    private void Start()
    {
        railsQueue = new Queue<GameObject>(rails);

    }

    // Update is called once per frame
    void Update()
    {
        if (travelBegun)
        {
            timeSinceStart += Time.deltaTime;
            int timer = (fullTimeLength - (int)timeSinceStart);
            int minutes = timer / 60;
            int seconds = timer % 60;
            string secondsString;
            if (seconds == 0)
            {
                secondsString = "00";
            }
            else if (seconds < 10)
            {
                secondsString = "0" + (timer % 60).ToString();
            }
            else
            {
                secondsString = (timer % 60).ToString();
            }
            if (minutes == 0)
            {
                timeRemaining.color = Color.red;
            }
            if (!gameOver)
            {
                timeRemaining.text = minutes.ToString() + ":" + secondsString;
            }

            if (behindOnTimeBoost && currentSpeed == 3)
            {
                travelDistance += (currentSpeed + 1) * Time.deltaTime;
                print("boosting");
               
            }
            else
            {
                travelDistance += currentSpeed * Time.deltaTime;
            }


            distanceTravelled.text = ((int)travelDistance).ToString() + "/" + fullTravelLength.ToString();
            ExpectedArrivalTime();


            OffSetGround();
            MoveRails();
            MakeSmoke();
            if (travelDistance > backgroundObjectMadeForDistance + 5)
            {
                backgroundObjectMadeForDistance = (int)travelDistance;
                MakeBackgroundObjects();
            }
            MoveBackgroundObjects();
            distanceUI.value = travelDistance / fullTravelLength;
            if (travelDistance >= fullTravelLength && !gameOver)
            {
                GameManager.GM.EndGame((int)timeSinceStart);
                gameOver = true;
            }
            else if (timeSinceStart >= fullTimeLength && !gameOver)
            {
                GameManager.GM.EndGame((int)timeSinceStart);
                gameOver = true;
            }
        }
    }

    public void StartTimer()
    {
        AudioManager.SharedInstance.PlayClip(0, 0.25f, true);
        AudioManager.SharedInstance.PlayClip(20, 0.1f, true);
       
        
        travelBegun = true;
    }

    public void AddDistance(int speed)
    {
        if (!travelBegun)
        {
            StartTimer();
        }
        currentSpeed = speed;
        offsetSpeed = offset;


        lerp = 0;
        offsetLerp = lerpTime * Mathf.Abs(currentSpeed - offset);
    }

    private void OffSetGround()
    {
        if (offset != currentSpeed)
        {
            lerp += Time.deltaTime / offsetLerp;
            offset = Mathf.Lerp(offsetSpeed, currentSpeed, lerp);
        }

        totalOffset -= offset * Time.deltaTime * offsetMultiplier;
        ground.material.SetTextureOffset("_MainTex", new Vector2(totalOffset, 0));
    }

    private void MoveRails()
    {
        foreach (GameObject rail in railsQueue)
        {
            rail.transform.Translate(Vector3.left * offset * Time.deltaTime * railsSpeed);

        }

        if (railsQueue.Peek().transform.position.x <= -95)
        {
            Vector3 newPos = railsQueue.Peek().transform.position;
            newPos.x = 80;
            GameObject rail = railsQueue.Dequeue();
            rail.transform.position = newPos;
            railsQueue.Enqueue(rail);
        }
    }

    private void MakeSmoke()
    {
        var emission = smoke.emission;
        emission.rateOverTime = currentSpeed * 10;
    }

    private void MakeBackgroundObjects()
    {
        int backgroundObject = Random.Range(0, 2);
        int randomPosition = Random.Range(0, backgroundObjectSpawnPosition.Length);
        if (backgroundObject == 0)
        {
            backgroundObjectsQueue.Enqueue(Instantiate(cactus, backgroundObjectSpawnPosition[randomPosition].position, Quaternion.Euler(-90, 0.0f, Random.Range(0.0f, 360.0f))));
        }
        else
        {
            backgroundObjectsQueue.Enqueue(Instantiate(rock, backgroundObjectSpawnPosition[randomPosition].position, Quaternion.Euler(-90, 0.0f, Random.Range(0.0f, 360.0f))));
        }
    }

    private void MoveBackgroundObjects()
    {
        if (backgroundObjectsQueue.Count > 0)
        {
            foreach (GameObject bgObject in backgroundObjectsQueue)
            {
                bgObject.transform.Translate(Vector3.left * offset * Time.deltaTime * railsSpeed, Space.World);

            }

            if (railsQueue.Peek().transform.position.x <= -95)
            {
                Destroy(railsQueue.Dequeue());
            }
        }
    }

    private void ExpectedArrivalTime()
    {
        float average = travelDistance / timeSinceStart;

        float expectedTimeInSeconds = ((fullTravelLength - travelDistance) / average);
    

        if ((fullTimeLength - timeSinceStart) < expectedTimeInSeconds)
        {
            behindOnTimeBoost = true;
        }
        else
        {
            behindOnTimeBoost = false;
        }
    }
}
