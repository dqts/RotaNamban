using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    [Header("Itens na cena")]
    public ItemVisualizador[] itensNaCena;

    // ✅ Desbloqueia um item diretamente pelo GameObject
    public void DesbloquearItem(string nomeItem)
    {
        // Verifica se é bule ou saque
        bool ehItemCondicional = nomeItem == "Item_Bule" || nomeItem == "Item_Saque";

        // Requisitos: arca aberta e na fase 4
        bool arcaAberta = PlayerPrefs.GetInt("Item_Arca", 0) == 1;
        bool naFase4 = PlayerPrefs.GetInt("FaseAtual", 0) == 4;

        if (ehItemCondicional && (!arcaAberta || !naFase4))
        {
            Debug.Log($"Item {nomeItem} bloqueado: requer arca aberta e estar na fase 4.");
            return; // Bloqueia a exibição
        }

        foreach (var item in itensNaCena)
        {
            if (item != null && item.name == nomeItem)
            {
                item.Desbloquear();
                return;
            }
        }

        Debug.LogWarning("Item '" + nomeItem + "' não encontrado no inventário.");
    }

    // ✅ Conta quantos itens foram desbloqueados
    public int TotalItensDesbloqueados()
    {
        int count = 0;
        foreach (var item in itensNaCena)
        {
            if (item != null && item.desbloqueado)
                count++;
        }
        return count;
    }

    // ✅ Reset: trava todos os itens
    public void ResetarInventario()
    {
        foreach (var item in itensNaCena)
        {
            if (item != null)
            {
                item.desbloqueado = false;
                item.GetComponent<UnityEngine.UI.Button>().interactable = false;
                item.gameObject.SetActive(false);
            }
        }
    }
}
