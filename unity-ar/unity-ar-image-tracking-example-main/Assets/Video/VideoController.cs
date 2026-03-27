using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        // Obtém o VideoPlayer do objeto filho
        videoPlayer = GetComponentInChildren<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("Nenhum VideoPlayer encontrado no objeto ou seus filhos!");
        }
    }

    private void OnEnable()
    {
        // Reproduz automaticamente quando o objeto for ativado
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    private void OnDisable()
    {
        // Para o vídeo quando o objeto for desativado
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }

    // Método opcional para ativar a reprodução manualmente, se necessário
    public void PlayVideo()
    {
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    // Método opcional para parar manualmente
    public void StopVideo()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }
}