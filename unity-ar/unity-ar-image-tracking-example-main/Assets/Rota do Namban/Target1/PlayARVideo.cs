using UnityEngine;
using UnityEngine.Video;

public class PlayARVideo : MonoBehaviour
{
    public VideoPlayer player;

    void OnEnable()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Namban.webm");

        player.source = VideoSource.Url;
        player.url = path;

        player.Prepare();
        player.prepareCompleted += _ => player.Play();
    }
}
