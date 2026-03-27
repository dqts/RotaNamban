using UnityEngine;
using TMPro;

public class UIContadorItens : MonoBehaviour
{
    [Header("Referências de Texto")]
    public TextMeshProUGUI textoInventario;
    public TextMeshProUGUI textoDiario;

    [Header("Configuração")]
    public int totalInventario = 6;
    public int totalAutocolantes = 5;

    private string[] nomesDosItens = new string[]
    {
        "Desbloqueado_Casaco",
        "Desbloqueado_Documento",
        "Desbloqueado_Arca",
        "Desbloqueado_Saque",
        "Desbloqueado_Bule",
        "Desbloqueado_Espada"
    };

    void Start()
    {
        AtualizarContadores();
    }

    public void AtualizarContadores()
{
    // Contador do Inventário
    int desbloqueadosInventario = 0;
    foreach (string chave in nomesDosItens)
    {
        if (PlayerPrefs.GetInt(chave, 0) == 1)
            desbloqueadosInventario++;
    }
    textoInventario.text = $"{desbloqueadosInventario}/{totalInventario}";

    // Contador do Diário (autocolantes via PlayerPrefs)
    int desbloqueadosDiario = 0;
    for (int i = 0; i < totalAutocolantes; i++)
    {
        if (PlayerPrefs.GetInt("Sticker_" + i, 0) == 1)
            desbloqueadosDiario++;
    }
    textoDiario.text = $"{desbloqueadosDiario}/{totalAutocolantes}";
}

}
