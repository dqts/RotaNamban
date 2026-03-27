using UnityEngine;

public class CartaMemoria : MonoBehaviour
{
    public string nomeCarta;
    public GameObject frente;
    public GameObject tras;
    public bool bloqueada = false;

    private static MemoriaGameManager gameManager; // Static: busca 1x só

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<MemoriaGameManager>();
        }
    }

    public void OnClick()
    {
        if (bloqueada || frente.activeSelf)
        {
            Debug.Log("Carta ignorada: bloqueada ou já está virada");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogWarning("GameManager não encontrado!");
            return;
        }

        if (!gameManager.PodeSelecionarCarta())
        {
            Debug.Log("Não pode selecionar mais cartas agora.");
            return;
        }

        Debug.Log("Carta clicada: " + nomeCarta);

        frente.SetActive(true);
        tras.SetActive(false);

        gameManager.SelecionarCarta(this);
    }

    public void VirarParaTrás()
    {
        frente.SetActive(false);
        tras.SetActive(true);
    }

    public void Bloquear()
    {
        bloqueada = true;
    }
}
