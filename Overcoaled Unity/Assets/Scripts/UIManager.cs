using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winScreen, loseScreen, gameOverScreen, star1, star2, star3;
    [SerializeField] private TextMeshProUGUI timeText, passengersText, totalScoreText;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void SetUI(int passengersLeft, int arrivalTime)
    {
        if (arrivalTime >= 180)
        {
            loseScreen.SetActive(true);
        }
        else
        {
            int totalScore = 0;
            winScreen.SetActive(true);

            if (arrivalTime <= 120)
            {
                timeText.text = "Early + 300pts";
                totalScore += 300;
            }
            else
            {
                timeText.text = "On Time + 100pts";
                totalScore += 100;
            }

            passengersText.text = passengersLeft.ToString() + " x50pts";
            totalScore += (passengersLeft * 50);
            totalScoreText.text = totalScore.ToString() + "pts";

            if (totalScore >= 100)
            {
                star1.SetActive(true);
            }
            if (totalScore >= 300)
            {
                star2.SetActive(true);
            }
            if (totalScore >= 500)
            {
                star3.SetActive(true);
            }
        }
    }
}
