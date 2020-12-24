using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDotsCount : MonoBehaviour
{
    public static SetDotsCount Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public GameObject DotObj;
    bool SetDiffrentNumOfDots = true;
    public void Set8()
    {
        if (SetDiffrentNumOfDots)
        {
            SetDiffrentNumOfDots = false;
            Instantiate(DotObj, transform);
        }
    }
}
