using UnityEngine;

public class InventarioRestaurador : MonoBehaviour
{
    public InventarioManager inventarioManager; // <- ADICIONE ISSO

    void Start()
    {
        RestaurarItensDesbloqueados();
    }

    void RestaurarItensDesbloqueados()
    {
        string[] nomesItens = { "Casaco", "Espada", "Documento", "Arca", "Bule", "Saque" };

        foreach (string nome in nomesItens)
        {
            if (PlayerPrefs.GetInt("Desbloqueado_" + nome, 0) == 1)
            {
                inventarioManager.DesbloquearItem(nome);
            }
        }
    }
}
