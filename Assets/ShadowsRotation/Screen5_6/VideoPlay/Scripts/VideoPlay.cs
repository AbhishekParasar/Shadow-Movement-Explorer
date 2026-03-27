using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoFileName; // Name of your video file in StreamingAssets

    void Start()
    {
        PlayVideoInWebGL();
    }

    public void PlayVideoInWebGL()
    {
        // Construct the correct path for WebGL StreamingAssets
        string videoPath = Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        videoPlayer.Play();
        Debug.Log("Attempting to play video from: " + videoPath);
    }
}

