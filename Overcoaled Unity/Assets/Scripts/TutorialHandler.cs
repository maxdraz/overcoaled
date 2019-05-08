using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialHandler : MonoBehaviour
{
   
    public List<string> positiveButtons;
    public List<string> negativeButtons;

    public List<GameObject> tutorialPages;

    private int pageCount;
    private int currentPage;
    private int lastPage;


    private void Start()
    {
        pageCount = tutorialPages.Count;
        lastPage = pageCount - 1;
        currentPage = 0;

        for(int i =0; i<tutorialPages.Count; i++)
        {
            if (i < 1)
            {
                tutorialPages[i].SetActive(true);
            }
            else
            {
                tutorialPages[i].SetActive(false);
            }
        }

    }

    private void Update()
    {
        //if forward button pressed
        foreach(string button in positiveButtons)
        {
            if (Input.GetButtonDown(button))
            {
                //go to next tutorial page
                GoToNextPage();

            }
        }

        //Back button pressed
        foreach (string button in negativeButtons)
        {
            if (Input.GetButtonDown(button))
            {
                //go to previous tutorial page
            }
        }

    }

    void GoToNextPage()
    {
        currentPage += 1;
        tutorialPages[currentPage].SetActive(true);
        tutorialPages[currentPage - 1].SetActive(false);
    }




}
