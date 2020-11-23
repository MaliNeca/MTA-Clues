using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCooldown());
    }

    private IEnumerator IntroCooldown()
    {
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene(1);
    }
}
