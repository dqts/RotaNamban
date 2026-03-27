using UnityEngine;
using UnityEngine.UI;

public class SceneSkipController : MonoBehaviour
{
    [Header("Configurações")]
    public float delayAntesDeMostrarBotao = 60f; // tempo em segundos até mostrar o botão

    [Header("Referências")]
    public Button botaoSkip;          // botão que será mostrado após o tempo
    public GameObject canvasParaAtivar; // canvas (ou qualquer GameObject) a ser ativado ao clicar

    private bool botaoMostrado = false;

    void Start()
    {
        if (botaoSkip != null)
            botaoSkip.gameObject.SetActive(false); // começa invisível

        // Inicia a contagem de tempo para mostrar o botão
        Invoke(nameof(MostrarBotao), delayAntesDeMostrarBotao);
    }

    void MostrarBotao()
    {
        if (botaoSkip != null)
        {
            botaoSkip.gameObject.SetActive(true);
            botaoSkip.onClick.AddListener(AoClicarBotao);
        }
        botaoMostrado = true;
    }

    void AoClicarBotao()
    {
        if (canvasParaAtivar != null)
            canvasParaAtivar.SetActive(true);

        // Desativar o botão se quiser
        botaoSkip.gameObject.SetActive(false);
    }
}
