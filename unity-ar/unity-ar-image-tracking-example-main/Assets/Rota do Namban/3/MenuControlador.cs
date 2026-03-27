using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuControlador : MonoBehaviour
{
    [Header("Referências de UI")]
    public GameObject fundoEscuro;
    public GameObject menuAberto;
    public RawImage botaoSomImage;
    public Texture somLigadoTexture;
    public Texture somDesligadoTexture;

    [Header("Objetos de Sobre")]
    public GameObject canvasCenaAtual; // <- arrasta o Canvas atual aqui
    public GameObject painelSobre;     // <- arrasta o painel Sobre aqui

    [Header("Cena Sobre (não será usada)")]
#if UNITY_EDITOR
    public SceneAsset cenaSobre;
#endif
    [SerializeField] private string nomeCenaSobre = "Sobre";

    private bool somAtivo = true;

    void Start()
    {
        // Verifica se já foi salvo o estado do som
        somAtivo = PlayerPrefs.GetInt("somAtivo", 1) == 1;
        AtualizarSomVisual();
        AplicarSom();
    }

    public void AbrirMenu()
    {
        fundoEscuro.SetActive(true);
        menuAberto.SetActive(true);
    }

    public void FecharMenu()
    {
        fundoEscuro.SetActive(false);
        menuAberto.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ToggleSom()
    {
        somAtivo = !somAtivo;
        PlayerPrefs.SetInt("somAtivo", somAtivo ? 1 : 0);
        PlayerPrefs.Save();

        AplicarSom();
        AtualizarSomVisual();
    }
public void VoltarDoSobre()
{
    if (canvasCenaAtual != null)
        canvasCenaAtual.SetActive(true);

    if (painelSobre != null)
        painelSobre.SetActive(false);
}

    void AplicarSom()
    {
        AudioListener.volume = somAtivo ? 1f : 0f;
    }

    void AtualizarSomVisual()
    {
        if (botaoSomImage != null)
        {
            botaoSomImage.texture = somAtivo ? somLigadoTexture : somDesligadoTexture;
        }
    }

    // Atualizado para ativar painel em vez de trocar de cena
    public void IrParaSobre()
    {
        if (canvasCenaAtual != null)
            canvasCenaAtual.SetActive(false);

        if (painelSobre != null)
            painelSobre.SetActive(true);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (cenaSobre != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaSobre);
            nomeCenaSobre = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}
