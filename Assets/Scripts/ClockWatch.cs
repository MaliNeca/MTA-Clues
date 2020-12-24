using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ClockWatch : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI ClockWatchText;
    private string clockWatchstring;
    private int minutes=20;
    private int seconds = 59;
    void Start()
    {
        StartCoroutine(SecCooldownSeconds());
        StartCoroutine(SetCooldownMinutes());
    }

    private IEnumerator SecCooldownSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds--;
            if (seconds < 0)
            {
                seconds = 60;
            }
            clockWatchstring=minutes + ":" + seconds;
            ClockWatchText.text = clockWatchstring;
        }
    }

    private IEnumerator SetCooldownMinutes()
    {
        while (true)
        {
            minutes--;
            if (minutes < 0)
            {
                minutes = 20;
            }
            clockWatchstring = minutes + ":" + seconds;
            ClockWatchText.text = clockWatchstring;
            yield return new WaitForSeconds(60);

        }
    }


}
