using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerInstance : MonoBehaviour
{
    public GameObject AudioControllerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioController.Instance == null)
        {
            Instantiate(AudioControllerPrefab);
        }
    }

    
}
