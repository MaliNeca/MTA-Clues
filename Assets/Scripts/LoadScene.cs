using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    static public int playerID;

    private void Start()
    {
        Application.ExternalEval("window.focus();");
    }
    private string[] pOneClues = {  /*1*/    "Trial-CV-CM-112020 is currently 28% effective",
                                    /*2*/    "Columbia Medical say progress on Trial-CV-CM-112020 is 3x faster than usual, and by the time it is developed it will be 3 times more effective than it is now",
                                        "Columbia Medical predict it will take 18 months to complete their vaccine development",
                                      /*1*/   "Trial-CV-CM-112020 is stored in the refrigeration unit at Columbia Medical",
                                        "Columbia Medical have produced a standard batch of 15'000 vaccines",
                                     /*2*/    "The projected cost per batch of Trial-CV-CM-112020 is $225'000",
                                     "CV-PM-112020 is 62% effective",
                     "ProtectionMax state that CV-PM-112020 should be kept cold at all times"
                                        };
    private string[] pTwoClues = {   /*1*/    "Colombia Medicine have recently been able to double how effective their vaccine is",
                                    /*4*/     "Previously, Generator was only 47% effective", //p4
                                        "Colombia Medicine anticipate that Generator will be ready in 8 months’ time",
                                     /*1*/    "Generator is stored in the secure refrigeration unit at the manufacturing facility",
                                   /*2*/      "The current cost per dose of Generator is $22.50",
                                   /*4*/     "By the time of manufacturing Colombia Medicine predict their new machinery will reduce the cost per dose by 75%",
                                   "Development of CV-PM-112020 will be finished in 20 weeks’ time",
                   "The increase in effectiveness that ProtectionMax hope to acheive will increase the cost per dose of their vaccine by 50%"
                                        };
    private string[] pThreeClues = { /*1*/    "Currently Elizabeth is 61% effective, and 3 further iterations of the vaccine are planned",
                                    /*4*/    "Each iteration of Elizabeth so far has increased the effectiveness by 8% points",
                                     /*2*/    "The final version of Elizabeth will be available in 5 months' time",
                                     /*1*/    "Elizabeth is stable when refrigerated",
                                        "Tests run by Eton Innovation show that Elizabeth is stable above 5 degrees C",
                                        "Eton Innovation project the cost per dose of their vaccine to be less than $10",
                                        "The cost per dose of CV-PM-112020 is currently set at $7.20"
                                        };
    private string[] pFourClues = {   /*2*/   "Viribus is 91% effective",
                                        "Boston Vaccines are very confident their vaccine will be ready in 9 months’ time",
                                   /*2*/      "Charlie Hainsworth says they can half the speed of development of Viribus for a cost per dose increase of $1.50",
                                        "Charlie Hainsworth of Boston Vaccines says the vaccine is effective when refrigerated",
                                        "Recent tests show that Viribus is stable up to 50 degrees C",
                                        "The projected cost per dose of Viribus is $8.00",
                                        "The effectiveness of CV-PM-112020 will be increase by 50% in the next two months"
                                        };

    private string[] pQuestionList =
    {
        "How would you rate the performance of the team out of 10? Think of specific examples to support your answer",
        "What would have needed to have happened for you to rank your team even higher?",
        "Rate your personal performance in the task out 10? Think of specific examples to support your answer",
        "What rating do you think your team would give you based on your performance?",
        "If the rating you feel the team would give you is different to the rating you gave yourself, what might explain the difference?",
        "What, as a result of this activity, will you do differently when you are back at work? what will the impact be?"
    };


    public static string[] playerOneClues;
    public static string[] playerTwoClues;
    public static string[] playerThreeClues;
    public static string[] playerFourClues;
    public static string[] playerQuestions;
    private ArrayList clueList;

    public void startGame(int pID)
    {
        AudioController.Instance.PlayClick();
        playerID = pID;
        clueList = new ArrayList();
        foreach (string c in pOneClues)
        {
            clueList.Add(c);
        }
        foreach (string c in pTwoClues)
        {
            clueList.Add(c);
        }
        foreach (string c in pThreeClues)
        {
            clueList.Add(c);
        }
        foreach (string c in pFourClues)
        {
            clueList.Add(c);
        }

        playerOneClues = getPlayerOneClues();
        playerTwoClues = getPlayerTwoClues();
        playerThreeClues = getPlayerThreeClues();
        playerFourClues = getPlayerFourClues();
        playerQuestions = getAllQuestions();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void backGame()
    {
        AudioController.Instance.PlayClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public string[] getPlayerOneClues()
    {
        string[] list = new string[8];
       

        for (int i=0; i<list.Length; i++)
        {
            list[i] = clueList[(2*i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }
       
        return list;

    }

    public string[] getPlayerTwoClues()
    {
        
        string[] list = new string[8];

        for (int i = 0; i < list.Length; i++)
        {
            list[i] = clueList[(2 * i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }

        return list;

    }
    public string[] getPlayerThreeClues()
    {
        string[] list = new string[7];
              
        for (int i = 0; i < list.Length; i++)
        {
            list[i] = clueList[(2 * i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }

        return list;

    }
    public string[] getPlayerFourClues()
    {
        string[] list = new string[7];

        for (int i = 0; i < list.Length; i++)
        {
            list[i] = clueList[i].ToString();
            

        }
       
        for(int i = 0; i< clueList.Count; i++)
        {
            clueList.RemoveAt(i);
        }
        return list;

    }

    public string[] getAllQuestions()
    {
        return pQuestionList;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
