using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class AnimationControl : MonoBehaviour
{
    public GameObject myAnimation;
    // public GameObject skipButton;
    VideoPlayer videoPlayer;
    float controlTime;

    void Start()
    {
        videoPlayer = myAnimation.GetComponent<VideoPlayer>();

        // skipButton.transform.GetComponent<Button>().onClick.AddListener(startGame);
        
        String sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "IntroVideoScene")
        {
            controlTime = 7;
        }
        Debug.Log(controlTime);
    }


    public void startGame()
    {
        videoPlayer.Pause();
        Debug.Log("skip");
        //SceneManager.LoadScene("TextDemoScene");
        GameObject.Find("Main Camera").GetComponent<DemoScript>().GoFade();
    }


    void Update()
    {
        // Debug.Log(videoPlayer.time);
        if (videoPlayer.time >= controlTime)
        {
            Debug.Log("change");
            //SceneManager.LoadScene("TextDemoScene");
            startGame();
        }
    }
}
