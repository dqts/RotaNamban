using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NavegacaoCondicional : MonoBehaviour
{
    [System.Serializable]
    public class Rota
    {
#if UNITY_EDITOR
        public SceneAsset cenaOrigemAsset;
        public SceneAsset cenaDestinoAsset;
#endif
        [HideInInspector] public string cenaOrigem;
        [HideInInspector] public string cenaDestino;
    }

    public List<Rota> rotas;
    public Button botao;

    private void Start()
    {
        if (botao != null)
            botao.onClick.AddListener(Navegar);
    }

    void Navegar()
    {
        string cenaAnterior = PlayerPrefs.GetString("CenaAnterior", "");
        foreach (var rota in rotas)
        {
            if (rota.cenaOrigem == cenaAnterior)
            {
                SceneManager.LoadScene(rota.cenaDestino);
                return;
            }
        }

        Debug.LogWarning("Nenhuma rota encontrada para: " + cenaAnterior);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var rota in rotas)
        {
            rota.cenaOrigem = rota.cenaOrigemAsset != null ? rota.cenaOrigemAsset.name : "";
            rota.cenaDestino = rota.cenaDestinoAsset != null ? rota.cenaDestinoAsset.name : "";
        }

        // Garante que o Unity salve as alterações
        EditorUtility.SetDirty(this);
    }
#endif
}