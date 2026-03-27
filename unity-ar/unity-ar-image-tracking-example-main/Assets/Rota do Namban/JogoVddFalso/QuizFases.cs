using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizFases : MonoBehaviour
{
    [System.Serializable]
    public class Fase
    {
        public Texture imagem;
        public string afirmacao;
        public bool respostaCorreta;
        public GameObject popupAcerto;
        public GameObject popupErro;
        public GameObject popupDica;
    }

    [Header("Popup Final")]
    public GameObject popupParabens;

    [Header("Popup Recompensa")]
    public GameObject popupEspada;

    [Header("Lista de Fases")]
    public List<Fase> fases;

    [Header("Referências da UI")]
    public RawImage imagemPrincipal;
    public TextMeshProUGUI textoAfirmacao;
    public TextMeshProUGUI textoProgresso;
    public Button botaoVerdadeiro;
    public Button botaoFalso;
    public GameObject[] botoesRawImages;

    [Header("Botão Dica")]
    public Button botaoDica;

    private int faseAtual = 0;

    void Start()
    {
        MostrarFase();

        botaoVerdadeiro.onClick.AddListener(() => VerificarResposta(true));
        botaoFalso.onClick.AddListener(() => VerificarResposta(false));
        botaoDica.onClick.AddListener(MostrarDica);
    }

    void MostrarFase()
    {
        if (faseAtual >= fases.Count) return;

        Fase fase = fases[faseAtual];

        imagemPrincipal.texture = fase.imagem;
        textoAfirmacao.text = fase.afirmacao;
        textoProgresso.text = $"{faseAtual + 1}/{fases.Count}";

        foreach (var f in fases)
        {
            if (f.popupErro != null) f.popupErro.SetActive(false);
            if (f.popupAcerto != null) f.popupAcerto.SetActive(false);
            if (f.popupDica != null) f.popupDica.SetActive(false);
        }
    }

    void VerificarResposta(bool respostaDoJogador)
    {
        Fase fase = fases[faseAtual];

        if (respostaDoJogador == fase.respostaCorreta)
        {
            if (fase.popupAcerto != null)
                fase.popupAcerto.SetActive(true);
        }
        else
        {
            if (fase.popupErro != null)
                fase.popupErro.SetActive(true);
        }
    }

    public void FecharPopupEAvancar()
    {
        Fase fase = fases[faseAtual];

        if (fase.popupAcerto != null) fase.popupAcerto.SetActive(false);
        if (fase.popupErro != null) fase.popupErro.SetActive(false);
        if (fase.popupDica != null) fase.popupDica.SetActive(false);

        if (faseAtual == fases.Count - 1)
        {
            if (popupParabens != null)
            {
                popupParabens.SetActive(true);
            }
            else
            {
                Debug.LogWarning("PopupParabens não atribuído no Inspector!");
            }
            return;
        }

        faseAtual++;
        MostrarFase();
    }

    public void FecharPopupParabensEEspada()
    {
        if (popupParabens != null)
            popupParabens.SetActive(false);

        if (popupEspada != null)
        {
            popupEspada.SetActive(true);

            PlayerPrefs.SetInt("Desbloqueado_Espada", 1);
            PlayerPrefs.Save();

           StickerSessionData.stickersParaDesbloquear.Add(0); // autocolante 0
    StickerSessionData.stickersParaDesbloquear.Add(1); // autocolante 0
 StickerSessionData.stickersParaDesbloquear.Add(2); // autocolante 2
 StickerSessionData.stickersParaDesbloquear.Add(3); // autocolante 2
  StickerSessionData.stickersParaDesbloquear.Add(4); // autocolante 2

  PlayerPrefs.SetInt("Sticker_0", 1);
PlayerPrefs.SetInt("Sticker_1", 1);
PlayerPrefs.SetInt("Sticker_2", 1);
PlayerPrefs.SetInt("Sticker_3", 1);
PlayerPrefs.SetInt("Sticker_4", 1);
PlayerPrefs.Save();
        }
    }

    public void FecharPopupEspada()
    {
        if (popupEspada != null)
            popupEspada.SetActive(false);
    }

    public void FecharPopupErro()
    {
        Fase fase = fases[faseAtual];

        if (fase.popupErro != null)
            fase.popupErro.SetActive(false);
    }

    void MostrarDica()
    {
        Fase fase = fases[faseAtual];

        if (fase.popupDica != null)
            fase.popupDica.SetActive(true);
    }

    public void FecharDica()
    {
        Fase fase = fases[faseAtual];

        if (fase.popupDica != null)
            fase.popupDica.SetActive(false);
    }
}