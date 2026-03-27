using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VideoFinal : MonoBehaviour
{
    [Header("Referências")]
    public VideoPlayer videoPlayer;
    public GameObject botaoProximaCena;

    [Header("Cena para Carregar")]
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar; // Usado só no Editor
#endif
    [SerializeField] private string nomeCena = "Cena3"; // Usado no runtime

    private bool jaAssistiuUmaVez = false;

    private void Start()
    {
        botaoProximaCena.SetActive(false); // Esconde no início
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (!jaAssistiuUmaVez)
        {
            botaoProximaCena.SetActive(true); // Mostra o botão apenas uma vez
            jaAssistiuUmaVez = true;
        }

        StartCoroutine(ReplayDepoisDeSegundos(5f));
    }

    IEnumerator ReplayDepoisDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        videoPlayer.Play(); // Dá replay, botão permanece visível
    }

    public void IrParaProximaCena()
    {
        if (!string.IsNullOrEmpty(nomeCena))
        {
            SceneManager.LoadScene(nomeCena);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (cenaParaCarregar != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaParaCarregar);
            nomeCena = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
