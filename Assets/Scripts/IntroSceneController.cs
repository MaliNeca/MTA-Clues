using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class IntroSceneController : MonoBehaviour
{
    public Button StartButton;
    // Start is called before the first frame update
    
    public void StartGame()
    {
        GetComponent<Animator>().enabled = true;
        StartCoroutine(IntroCooldown());
        StartButton.gameObject.SetActive(false);
    }

    private IEnumerator IntroCooldown()
    {
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene(1);
    }
}
