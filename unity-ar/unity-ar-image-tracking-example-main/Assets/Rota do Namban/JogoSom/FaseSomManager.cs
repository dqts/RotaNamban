using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class FaseSomManager : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public string textoPergunta;
        public GameObject[] botoes;
        public RawImage[] imagensDosBotoes;
        public Texture[] imagens;
        public int indexCorreto;
        public GameObject[] popupsErrados;
        public GameObject popupCerto;
        public GameObject popupDica;
        public GameObject popupExtraDepoisDeCerto;
        public AudioClip audioPergunta;
    }

    public List<Fase> fases;
    public TextMeshProUGUI textoPerguntaUI;
    public TextMeshProUGUI progressoText;
    public GameObject parabensPopup;
    public string nomeCenaFinal;

    public Button botaoAudioPergunta;
    public AudioSource audioSource;

    private int faseAtual = 0;

    void Start()
    {
        MostrarFase();
    }

    void MostrarFase()
    {
        foreach (var fase in fases)
        {
            if (fase.popupDica != null)
                fase.popupDica.SetActive(false);
            foreach (var p in fase.popupsErrados)
                p.SetActive(false);
            if (fase.popupCerto != null)
                fase.popupCerto.SetActive(false);
        }

        var f = fases[faseAtual];
        textoPerguntaUI.text = f.textoPergunta;
        progressoText.text = $"{faseAtual + 1}/3";

        botaoAudioPergunta.onClick.RemoveAllListeners();
        botaoAudioPergunta.onClick.AddListener(() => TocarAudioPergunta(f.audioPergunta));

        for (int i = 0; i < f.botoes.Length; i++)
        {
            var btnObj = f.botoes[i];
            var btn = btnObj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            int index = i;
            btn.onClick.AddListener(() => CliqueBotao(index));

            if (f.imagensDosBotoes.Length > i && f.imagens.Length > i && f.imagensDosBotoes[i] != null)
            {
                f.imagensDosBotoes[i].texture = f.imagens[i];
            }
        }

        // 🔄 Salva a fase atual em PlayerPrefs
        PlayerPrefs.SetInt("FaseAtual", faseAtual + 1); // +1 porque começa de zero
        PlayerPrefs.Save();
    }

    public void AvancarDoPopupCerto()
    {
        var f = fases[faseAtual];
        f.popupCerto.SetActive(false);

        if (f.popupExtraDepoisDeCerto != null)
        {
            f.popupExtraDepoisDeCerto.SetActive(true);
        }
        else
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

    public void FecharPopupExtraEAvancar()
    {
        var f = fases[faseAtual];

        if (f.popupExtraDepoisDeCerto != null)
            f.popupExtraDepoisDeCerto.SetActive(false);

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

    void TocarAudioPergunta(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
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

         StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0
    StickerSessionData.stickersParaDesbloquear.Add(1); // autocolante 0
 StickerSessionData.stickersParaDesbloquear.Add(2); // autocolante 2
 StickerSessionData.stickersParaDesbloquear.Add(3); // autocolante 2

 PlayerPrefs.SetInt("Sticker_0", 1);
PlayerPrefs.SetInt("Sticker_1", 1);
PlayerPrefs.SetInt("Sticker_2", 1);
PlayerPrefs.SetInt("Sticker_3", 1);
PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(nomeCenaFinal))
        {
            SceneManager.LoadScene(nomeCenaFinal);
        }
        else
        {
            Debug.LogWarning("Nome da cena final não foi definido!");
        }
    }

    public void FecharPopup(GameObject popup)
    {
        popup.SetActive(false);

        
         StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0
    StickerSessionData.stickersParaDesbloquear.Add(1); // autocolante 0
 StickerSessionData.stickersParaDesbloquear.Add(2); // autocolante 2
 StickerSessionData.stickersParaDesbloquear.Add(3); // autocolante 2

  PlayerPrefs.SetInt("Sticker_0", 1);
PlayerPrefs.SetInt("Sticker_1", 1);
PlayerPrefs.SetInt("Sticker_2", 1);
PlayerPrefs.SetInt("Sticker_3", 1);
PlayerPrefs.Save();

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
}