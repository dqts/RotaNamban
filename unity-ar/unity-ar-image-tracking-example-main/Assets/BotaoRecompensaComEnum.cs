using UnityEngine;
using UnityEngine.UI;

public class BotaoRecompensaComEnum : MonoBehaviour
{
    public enum ItemRecompensa
    {
        Casaco,
        Espada,
        Documento,
        Arca,
        Bule,
        Saque
    }

    [Header("Escolher item da recompensa")]
    public ItemRecompensa itemSelecionado;

    private Button botao;

    void Start()
    {
        botao = GetComponent<Button>();

        if (botao != null)
            botao.onClick.AddListener(SalvarRecompensa);
        else
            Debug.LogWarning("BotaoRecompensa: Nenhum componente Button encontrado!");
    }

    void SalvarRecompensa()
    {
        string nomeItem = itemSelecionado.ToString();
        PlayerPrefs.SetInt("Desbloqueado_" + nomeItem, 1); // ✅ Grava que o item foi ganho
        PlayerPrefs.Save();
 VerificarProgresso(); 
        Debug.Log("Item " + nomeItem + " marcado como desbloqueado.");
    }
    void VerificarProgresso()
{
    // Parte 1: Casaco, Documento, Arca
    if (
        PlayerPrefs.GetInt("Desbloqueado_Casaco", 0) == 1 &&
        PlayerPrefs.GetInt("Desbloqueado_Documento", 0) == 1 &&
        PlayerPrefs.GetInt("Desbloqueado_Arca", 0) == 1)
    {
        PlayerPrefs.SetInt("Entregues_Parte1", 1);
    }

    // Parte 2: Saque, Bule, Espada
    if (
        PlayerPrefs.GetInt("Desbloqueado_Saque", 0) == 1 &&
        PlayerPrefs.GetInt("Desbloqueado_Bule", 0) == 1 &&
        PlayerPrefs.GetInt("Desbloqueado_Espada", 0) == 1)
    {
        PlayerPrefs.SetInt("Entregues_Parte2", 1);
    }

    PlayerPrefs.Save();
}

}
