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

        StartTutorial();

    }

    private void Update()
    {
        //if forward button pressed
        foreach(string button in positiveButtons)
        {
            //if not on last page
            if (currentPage == lastPage && Input.GetKeyDown(KeyCode.X))
            {
                ExitTutorial();
            }
            else if(currentPage < lastPage && Input.GetKeyDown(KeyCode.X))// if on last page
            {
                GoToNextPage();
            }
        }

        //Back button pressed
        foreach (string button in negativeButtons)
        {
            if (currentPage > 0)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    //go to previous tutorial page
                    GoToPreviousPage();
                }
            }
            else
            {
                return;
            }
        }

    }

    void GoToNextPage()
    {
        currentPage += 1;
        tutorialPages[currentPage].SetActive(true);
        tutorialPages[currentPage - 1].SetActive(false);
    }

    void GoToPreviousPage()
    {
        currentPage -= 1;
        tutorialPages[currentPage].SetActive(true);
        tutorialPages[currentPage + 1].SetActive(false);
    }

    void StartTutorial()
    {        
        Time.timeScale = 0;
        currentPage = 0;
        for (int i = 0; i < tutorialPages.Count; i++)
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

    void ExitTutorial()
    {
        tutorialPages[currentPage].SetActive(false);
        Time.timeScale = 1;
        currentPage = 0;
    }



}
