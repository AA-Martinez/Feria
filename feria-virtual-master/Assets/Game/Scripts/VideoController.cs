using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer displayVideoPlayer;
    public TextMesh timeDisplay;

    private string videoURL = "http://techslides.com/demos/sample-videos/small.mp4";
    //private string videoURL = "https://www.youtube.com/watch?v=ibd80uKiCZs";
    private string videoFilePath = "/Game/Resource/Undertale Orchestrated - Bergentrückung & ASGORE.mp4";
    private VideoPlayer fullScreenVideoPlayer;
    private bool displayVideoIsPaused = false;

    void Start()
	{
		videoFilePath = Application.dataPath + videoFilePath;
    }

    void Update()
    {
        
        if (fullScreenVideoPlayer && fullScreenVideoPlayer.isPlaying)
        {
            
            if (Input.GetKey(KeyCode.Escape))
            {
                
                fullScreenVideoPlayer.Stop();
            }
        }

        
        if (displayVideoPlayer && timeDisplay)
        {
            DisplayTime();
        }
    }

    private void DisplayTime()
    {
        
        string minutes = Mathf.Floor ((int)displayVideoPlayer.time / 60).ToString ("00");
        string seconds = ((int)displayVideoPlayer.time % 60).ToString ("00");
        string lengthMinutes = Mathf.Floor ((int)displayVideoPlayer.clip.length / 60).ToString ("00");
        string lengthSeconds = ((int)displayVideoPlayer.clip.length % 60).ToString ("00");
        
        timeDisplay.text = minutes + ":" + seconds + " / " + lengthMinutes + ":" +
            lengthSeconds;
    }

    public void PlayInWorldVideo()
    {
        
        if (!displayVideoIsPaused) {
            
            displayVideoPlayer.playOnAwake = false;
            displayVideoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            displayVideoPlayer.url = videoFilePath;
            displayVideoPlayer.frame = 0;
            displayVideoPlayer.isLooping = true;
        }

        
        displayVideoPlayer.Play();
        
        displayVideoIsPaused = false;
    }

    public void PauseInWorldVideo()
    {
        
        if (displayVideoIsPaused)
        {
            
            displayVideoPlayer.Play();
            displayVideoIsPaused = false;
        }
        else
        {
            
            displayVideoPlayer.Pause();
            displayVideoIsPaused = true;
        }
    }

    public void StopInWorldVideo()
    {
        
        displayVideoPlayer.Stop();
        
        displayVideoIsPaused = false;
    }

    public void PlayFullScreenOnlineVideo()
	{
        StartFullScreenVideo(videoURL);
	}

    
    public void PlayFullScreenOfflineVideo()
    {
        StartFullScreenVideo(videoFilePath);
    }

    private void StartFullScreenVideo(string path)
    {
        
        if (fullScreenVideoPlayer)
        {
            Destroy(fullScreenVideoPlayer);
        }
        
        
        fullScreenVideoPlayer = Camera.main.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        
        fullScreenVideoPlayer.playOnAwake = false;
        fullScreenVideoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        fullScreenVideoPlayer.targetCameraAlpha = 1F;
        fullScreenVideoPlayer.url = path;
        fullScreenVideoPlayer.frame = 0;
        fullScreenVideoPlayer.isLooping = false;
        
        fullScreenVideoPlayer.Play();
    }
}
