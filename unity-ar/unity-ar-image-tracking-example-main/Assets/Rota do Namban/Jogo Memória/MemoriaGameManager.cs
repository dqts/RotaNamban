using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoriaGameManager : MonoBehaviour
{
    [System.Serializable]
    public class ParInfo
    {
        public string nome;
        public string descricao;
        public GameObject imagemGO; // Novo: referência ao GameObject da imagem
    }

    public List<ParInfo> paresInfo;
    public string sceneToLoad; // Define no Inspector o nome da próxima cena

    public GameObject popupExplicacao;
    public TextMeshProUGUI tituloExplicacao;
    public TextMeshProUGUI descricaoExplicacao;

    public GameObject popupParabens;
    public GameObject popupBau;
    public GameObject popupDica;

    private int paresEncontrados = 0;
    private int totalPares;

    private CartaMemoria primeiraCarta;
    private CartaMemoria segundaCarta;

    private bool bloqueado = false; // Bloqueia seleção enquanto comparando

    void Start()
    {
        totalPares = paresInfo.Count;
    }

    public void MostrarParEncontrado(string nome)
    {
        ParInfo par = paresInfo.Find(p => p.nome == nome);
        if (par != null)
        {
            // Desativa todos os GameObjects de imagem
            foreach (var item in paresInfo)
            {
                if (item.imagemGO != null)
                    item.imagemGO.SetActive(false);
            }

            // Ativa o GameObject da imagem correspondente
            if (par.imagemGO != null)
                par.imagemGO.SetActive(true);

            tituloExplicacao.text = par.nome;
            descricaoExplicacao.text = par.descricao;

            // Ajustes específicos se for "Cadeira"
            if (par.nome == "Cadeira")
            {
                descricaoExplicacao.fontSize = 59.64f;
                descricaoExplicacao.enableAutoSizing = false;

                RectTransform rt = descricaoExplicacao.GetComponent<RectTransform>();
                if (rt != null)
                {
                    Vector2 pos = rt.anchoredPosition;
                    pos.y = -194.6f;
                    rt.anchoredPosition = pos;
                }
            }

            popupExplicacao.SetActive(true);
        }
    }

    public void ConfirmarPar()
    {
        popupExplicacao.SetActive(false);

        paresEncontrados++;
        if (paresEncontrados == totalPares)
        {
            popupParabens.SetActive(true);
        }

        primeiraCarta = null;
        segundaCarta = null;
        bloqueado = false;
    }

    public void FecharParabens()
    {
        popupParabens.SetActive(false);
        popupBau.SetActive(true);
    }

    public void FecharBau()
    {
        // Adiciona o índice do sticker a desbloquear
        StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0

          PlayerPrefs.SetInt("Sticker_0", 1);
    PlayerPrefs.Save();

        // Muda de cena
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena definida para carregar.");
        }
    }

    public void MostrarDica()
    {
        popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        popupDica.SetActive(false);
    }

    public void SelecionarCarta(CartaMemoria carta)
    {
        if (primeiraCarta == null)
        {
            primeiraCarta = carta;
        }
        else if (segundaCarta == null && carta != primeiraCarta)
        {
            segundaCarta = carta;
            bloqueado = true; // Bloqueia mais cliques enquanto compara
            StartCoroutine(CompararCartas());
        }
    }

    public bool PodeSelecionarCarta()
    {
        return !bloqueado && (primeiraCarta == null || segundaCarta == null);
    }

    private IEnumerator CompararCartas()
    {
        yield return new WaitForSeconds(1f);

        if (primeiraCarta.nomeCarta == segundaCarta.nomeCarta)
        {
            primeiraCarta.Bloquear();
            segundaCarta.Bloquear();
            MostrarParEncontrado(primeiraCarta.nomeCarta);
        }
        else
        {
            primeiraCarta.VirarParaTrás();
            segundaCarta.VirarParaTrás();
            primeiraCarta = null;
            segundaCarta = null;
            bloqueado = false;
        }
    }
}