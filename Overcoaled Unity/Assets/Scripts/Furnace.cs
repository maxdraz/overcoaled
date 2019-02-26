using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{
    [SerializeField] private int coalCount;
    [SerializeField] private int maxCoal;
    private TextMesh coalText;
    [SerializeField] private float burnCD = 3f;
    [SerializeField] private float burnCDRemaining;
    [SerializeField] private TravelManager travelManager;
    public Sprite[] sprites;
    public List <SpriteRenderer> coalIcons;

    // Start is called before the first frame update
    void Start()
    {
        coalText = transform.GetComponentInChildren<TextMesh>();
        coalText.text = "Coal: " + coalCount.ToString() + "/" +  maxCoal.ToString();
        burnCDRemaining = burnCD;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        if(coalCount > 0)
        {
            burnCDRemaining -= Time.deltaTime;

            if(burnCDRemaining <= 0)
            {
                coalCount -= 1;
                travelManager.AddDistance(coalCount);
                coalText.text = "Coal: " + coalCount.ToString() + "/" + maxCoal.ToString();
                burnCDRemaining = burnCD;
                coalIcons[coalCount].sprite = sprites[0];
            }
        }
    }

    public void AddCoal(int amount)
    {
        if (!CheckIfCanAdd())
        {
            return;
        }
        else
        {
            coalCount += amount;
            travelManager.AddDistance(coalCount);
            coalText.text = "Coal: " + coalCount.ToString() + "/" + maxCoal.ToString();

            coalIcons[coalCount - 1].sprite = sprites[1];


        }
    }

    public bool CheckIfCanAdd()
    {
        if (coalCount >= maxCoal)
        {
            return false;
        }
        else return true;
    }
}
