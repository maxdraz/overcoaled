using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelManager : MonoBehaviour
{

    [SerializeField] private int fullTravelLength;
    [SerializeField] private int fullTimeLength;
    [SerializeField] public float travelDistance = 0;
    [SerializeField] private float timeSinceStart = 0;
    private bool travelBegun;
    private int currentSpeed;
    private float offsetSpeed;
    [SerializeField] private float offset;
    [SerializeField] private float lerpTime;
    [SerializeField] private int offsetMultiplier;
    [SerializeField] private float lerp = 0;
    private float totalOffset;
    private float offsetLerp;

    [SerializeField] private Renderer ground;
    // Update is called once per frame
    void Update()
    {
        if (travelBegun)
        {
            timeSinceStart += Time.deltaTime;
            travelDistance += currentSpeed * Time.deltaTime;
            OffSetGround();
            if (travelDistance >= fullTravelLength)
            {
                //End game
            }
            else if (timeSinceStart >= fullTimeLength)
            {
                //Game over
            }
        }
    }

    public void StartTimer()
    {
        travelBegun = true;
    }

    public void AddDistance(int speed)
    {
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
}
