using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class VidePlayerWebGlFix : MonoBehaviour
{
    public string VideoName;
    private VideoPlayer myVideoPlayer;
    public Button ReferenceButton;
    // Start is called before the first frame update
    void Awake()
    {
        myVideoPlayer = GetComponent<VideoPlayer>();
        myVideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, (VideoName+".webm"));
    }

    private void Start()
    {
        if (ReferenceButton != null)
        {
            ReferenceButton.onClick.Invoke();
        }
        myVideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, (VideoName + ".webm"));
        StartCoroutine(WaitForVideo());
    }
    
    private IEnumerator WaitForVideo()
    {
        yield return new WaitForSecondsRealtime(1);
        if (!myVideoPlayer.isPlaying)
        {
            myVideoPlayer.Play();
        }
    }


}
