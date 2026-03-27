using UnityEngine;

public class SequenciaFinalItens : MonoBehaviour
{
    public InventarioManager inventarioManager;

    [Header("Itens mostrados primeiro (se tiver os 6)")]
    public GameObject[] primeiraFase; // Arca, Documento, Casaco

    [Header("Itens mostrados depois de abanar o casaco")]
    public GameObject[] segundaFase; // Saque, Bule, Espada

    private bool jaMostrouPrimeiraFase = false;

    void Start()
    {
        // Espera o Inventario estar completo
        if (inventarioManager != null && inventarioManager.TotalItensDesbloqueados() == 6)
        {
            MostrarPrimeiraFase();
        }
    }

    public void MostrarPrimeiraFase()
    {
        foreach (var obj in primeiraFase)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (var obj in segundaFase)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        jaMostrouPrimeiraFase = true;
    }

    public void MostrarSegundaFase()
    {
        if (jaMostrouPrimeiraFase)
        {
            foreach (var obj in segundaFase)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }
    }
}
