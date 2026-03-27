using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicaGlobal : MonoBehaviour
{
    public static MusicaGlobal instancia;

    [Header("Música")]
    public AudioSource musica;

    [Header("Cenas onde a música não deve tocar")]
    public List<string> cenasSemMusica = new List<string>();

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // Garante só uma instância
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        if (cenasSemMusica.Contains(cena.name))
        {
            if (musica.isPlaying)
                musica.Pause();
        }
        else
        {
            if (!musica.isPlaying)
                musica.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
