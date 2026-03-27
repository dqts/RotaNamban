using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbacoVerificador : MonoBehaviour
{
    [Header("Contas Superiores")]
    public TopBeadController contaTopoDireita;

    [Header("Contas Inferiores")]
    public BeadController contaBaixoEsquerda;
    public BeadController contaBaixoDireita;

    [Header("Texturas Amarelas (Highlight)")]
    public RawImage imagemTopoDireita;
    public RawImage imagemBaixoEsquerda;
    public RawImage imagemBaixoDireita;
    public Texture texturaAmarela;

    [Header("Popups")]
    public GameObject popupCanvas;      // Popup certo (CanvasParabens)
    public GameObject popupErrado;      // Popup errado (CanvasErrado)

    public void VerificarAbaco()
    {
        Debug.Log("VerificarAbaco chamado!");

        // Definições das posições corretas
        float posTopoDireita = 133.02f;
        float posBaixo = -39.7f;
        float margem = 0.5f; // margem de erro aceitável

        bool topoOk = contaTopoDireita != null &&
                      Mathf.Abs(contaTopoDireita.GetComponent<RectTransform>().anchoredPosition.y - posTopoDireita) < margem;

        bool baixoEsquerdaOk = contaBaixoEsquerda != null &&
                      Mathf.Abs(contaBaixoEsquerda.GetY() - posBaixo) < margem;

        bool baixoDireitaOk = contaBaixoDireita != null &&
                      Mathf.Abs(contaBaixoDireita.GetY() - posBaixo) < margem;

        Debug.Log("Topo OK: " + topoOk);
        Debug.Log("Esquerda OK: " + baixoEsquerdaOk);
        Debug.Log("Direita OK: " + baixoDireitaOk);

        if (topoOk && baixoEsquerdaOk && baixoDireitaOk)
        {
            StartCoroutine(MostrarParabensComDelay());
        }
        else
        {
            if (popupErrado != null)
                popupErrado.SetActive(true);
        }
    }

   private IEnumerator MostrarParabensComDelay()
{
        StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0
    StickerSessionData.stickersParaDesbloquear.Add(1); // autocolante 0

PlayerPrefs.SetInt("Sticker_0", 1);
    PlayerPrefs.SetInt("Sticker_1", 1);
    PlayerPrefs.Save();

    // Trocar texturas para amarelo imediatamente
    if (imagemTopoDireita != null)
        imagemTopoDireita.texture = texturaAmarela;

    if (imagemBaixoEsquerda != null)
        imagemBaixoEsquerda.texture = texturaAmarela;

    if (imagemBaixoDireita != null)
        imagemBaixoDireita.texture = texturaAmarela;

    // Esperar 1.5 segundos antes de mostrar o popup
    yield return new WaitForSeconds(1.5f);

    if (popupCanvas != null)
        popupCanvas.SetActive(true);
}

}
