using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class FaseManager : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public string textoPergunta;
        public RawImage[] imagensBotoes;           // 4 imagens com botões
        public int indexCorreto;                   // índice do botão certo
        public GameObject[] popupsErrados;         // popups para cada erro
        public GameObject popupCerto;              // popup quando acerta
        public GameObject popupDica;               // dica da fase (personalizada)
        public Texture[] imagens;                  // imagens mostradas nos botões
    }

    public List<Fase> fases;
    public TextMeshProUGUI textoPerguntaUI;
    public TextMeshProUGUI progressoText;
    public GameObject parabensPopup;
    public GameObject bauPopup;
    public string nomeCenaFinal; // Nome da cena a carregar ao final
    private int faseAtual = 0;

    void Start()
    {
        MostrarFase();
    }

    void MostrarFase()
    {
        // Esconde todos os popups
        foreach (var fase in fases)
        {
            if (fase.popupDica != null)
                fase.popupDica.SetActive(false);
            foreach (var p in fase.popupsErrados)
                p.SetActive(false);
            if (fase.popupCerto != null)
                fase.popupCerto.SetActive(false);
        }

        // Atualiza UI
        var f = fases[faseAtual];
        textoPerguntaUI.text = f.textoPergunta;
        progressoText.text = $"{faseAtual + 1}/3";

        for (int i = 0; i < f.imagensBotoes.Length; i++)
        {
            f.imagensBotoes[i].texture = f.imagens[i];
            var btn = f.imagensBotoes[i].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            int index = i;
            btn.onClick.AddListener(() => CliqueBotao(index));
        }
    }

    void CliqueBotao(int index)
    {
        var f = fases[faseAtual];
        if (index == f.indexCorreto)
        {
            f.popupCerto.SetActive(true);
        }
        else
        {
            f.popupsErrados[index].SetActive(true);
        }
    }

    public void MostrarDica()
    {
        foreach (var fase in fases)
            if (fase.popupDica != null)
                fase.popupDica.SetActive(false);

        fases[faseAtual].popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        fases[faseAtual].popupDica.SetActive(false);
    }

    public void ContinuarPosParabens()
    {
        parabensPopup.SetActive(false);
        bauPopup.SetActive(true);
    }

    public void FecharPopup(GameObject popup)
    {
        popup.SetActive(false);

        if (popup == fases[faseAtual].popupCerto)
        {
            if (faseAtual == fases.Count - 1)
            {
                parabensPopup.SetActive(true);
            }
            else
            {
                faseAtual++;
                MostrarFase();
            }
        }
    }

    // ✅ NOVO: Ao fechar baú, adiciona sticker e carrega nova cena
    public void FecharBau()
    {
        // Adiciona o índice do sticker a desbloquear
           StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0
    StickerSessionData.stickersParaDesbloquear.Add(1); // autocolante 0
 StickerSessionData.stickersParaDesbloquear.Add(2); // autocolante 2

PlayerPrefs.SetInt("Sticker_0", 1);
PlayerPrefs.SetInt("Sticker_1", 1);
PlayerPrefs.SetInt("Sticker_2", 1);
PlayerPrefs.Save();

        // Troca de cena
        if (!string.IsNullOrEmpty(nomeCenaFinal))
        {
            SceneManager.LoadScene(nomeCenaFinal);
        }
        else
        {
            Debug.LogWarning("Nome da cena final não foi definido!");
        }
    }
}
