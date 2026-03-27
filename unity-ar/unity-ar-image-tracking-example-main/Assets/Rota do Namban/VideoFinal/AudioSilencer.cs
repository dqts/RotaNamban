using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSilencer : MonoBehaviour
{
    void Start()
    {
        // Obtém a cena atual
        Scene cenaAtual = SceneManager.GetActiveScene();

        // Pega todos os AudioSources carregados
        AudioSource[] todosAudios = FindObjectsOfType<AudioSource>(true);

        foreach (AudioSource audio in todosAudios)
        {
            // Verifica se o objeto que contém o AudioSource está em outra cena
            if (audio.gameObject.scene.name != cenaAtual.name)
            {
                audio.mute = true; // Silencia
            }
        }
    }
}