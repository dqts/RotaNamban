using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SelecaoPersonagem : MonoBehaviour
{
    [Header("Personagens (RawImages ou GameObjects)")]
    public GameObject[] personagens;

    [Header("Botões")]
    public GameObject botaoAnterior;
    public GameObject botaoProximo;

    private int indexAtual = 0;

    [Header("Cena para seguir")]
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar;
#endif
    [SerializeField] private string nomeCena = "Cena4";

    void Start()
    {
        AtualizarVisual();
    }

    public void Proximo()
    {
        indexAtual = (indexAtual + 1) % personagens.Length;
        AtualizarVisual();
    }

    public void Anterior()
    {
        indexAtual = (indexAtual - 1 + personagens.Length) % personagens.Length;
        AtualizarVisual();
    }

    void AtualizarVisual()
    {
        for (int i = 0; i < personagens.Length; i++)
            personagens[i].SetActive(i == indexAtual);
    }

    public void ConfirmarEscolha()
    {
        PlayerPrefs.SetInt("personagemSelecionado", indexAtual); // Guarda a escolha
        SceneManager.LoadScene(nomeCena);
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
