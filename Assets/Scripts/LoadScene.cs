using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    static public int playerID;

    //container with Clues
    private string[] pOneClues = {  /*1*/    "Trial-CV-CM-112020 is currently 26% effective.",
                                    /*2*/    "Columbia Medical say progress on Trial-CV-CM-112020 is 3x faster than usual, and by the time it is developed it will be 3 times more effective than it is now.",
                                        "Columbia Medical predict it will take 18 months to complete their vaccine development.",
                                      /*1*/   "Trial-CV-CM-112020 is stored in the refrigeration unit at Columbia Medical.",
                                        "Columbia Medical have produced a standard batch of 15'000 vaccines.",
                                     /*2*/    "The projected cost per batch of Trial-CV-CM-112020 is $22'500"
                                        };
    private string[] pTwoClues = {   /*1*/    "Colombia Medicine have recently been able to double their vaccine",
                                    /*4*/     "Previously, Generator was only 47% effective", //p4
                                        "Colombia Medicine anticipate their vaccine will be ready in 8 months’ time",
                                     /*1*/    "Generator is stored in the secure refrigeration unit at the facility",
                                   /*2*/      "The currently cost per dose of Generator is $2.25",
                                   /*4*/     "By the time of manufacturing Colombia Medicine predict their new machinery will reduce the cost per dose by 75%"
                                        };
    private string[] pThreeClues = { /*1*/    "Currently Elizabeth is 51% effective, and we have 3 further iterations of the vaccine planned",
                                    /*4*/    "Each iteration of Elizabeth has so far has increase its effectiveness by 6% points",
                                     /*2*/    "The final version of Elizabeth will be available in 5 months’ time",
                                     /*1*/    "Elizabeth is stable when refrigerated",
                                        "The test run by Eton Innovation show that Elizabeth is stable above 5 degrees C",
                                        "Eton Innovation project the cost per dose of their vaccine to be less than one 1$"
                                        };
    private string[] pFourClues = {   /*2*/   "The Viribus is 91% effective",
                                        "Boston Vaccines are very confident their vaccine will be ready in 9 months’ time",
                                   /*2*/      "Charlie Hainsworth says they can half the speed of development for a cost per dose increase of $0.15",
                                        "Charlie Hainsworth of Boston Vaccines shows the vaccine is effective when refrigerated",
                                        "Recent tests show that Viribus is stable up to 50 degrees C",
                                        "The projected cost per dose of Viribus is $.80"
                                        };

    private string[] pQuestionList =
    {
        "Question1",
        "Question2",
        "Question3",
        "Question4",
        "Question5",
        "Question6"
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
        string[] list = new string[6];
       

        for (int i=0; i<list.Length; i++)
        {
            list[i] = clueList[(2*i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }
       
        return list;

    }

    public string[] getPlayerTwoClues()
    {
        string[] list = new string[6];

        for (int i = 0; i < list.Length; i++)
        {
            list[i] = clueList[(2 * i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }

        return list;

    }
    public string[] getPlayerThreeClues()
    {
        string[] list = new string[6];
              
        for (int i = 0; i < list.Length; i++)
        {
            list[i] = clueList[(2 * i) % clueList.Count].ToString();
            clueList.RemoveAt((2 * i) % clueList.Count);

        }

        return list;

    }
    public string[] getPlayerFourClues()
    {
        string[] list = new string[6];

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
}
