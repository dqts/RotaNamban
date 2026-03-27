using UnityEngine;
using UnityEngine.UI;

public class MostrarPersonagemSelecionado : MonoBehaviour
{
    [Header("Destinos onde a imagem será mostrada")]
    public RawImage[] rawImagesDestino;

    [Header("Texturas disponíveis")]
    public Texture[] texturasDosPersonagens;

    void Start()
    {
        int indice = PlayerPrefs.GetInt("personagemSelecionado", 0);
        if (indice >= 0 && indice < texturasDosPersonagens.Length)
        {
            Texture personagem = texturasDosPersonagens[indice];

            // Aplica a imagem a todos os RawImages destino
            foreach (RawImage rawImage in rawImagesDestino)
            {
                if (rawImage != null)
                {
                    rawImage.texture = personagem;
                }
            }
        }
    }
}
