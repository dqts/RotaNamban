using UnityEngine;

public class AbrirMenu : MonoBehaviour
{
    [Header("Referências")]
    public GameObject fundoTransparente;
    public GameObject painelMenu;

    public void Abrir()
    {
        fundoTransparente.SetActive(true);
        painelMenu.SetActive(true);
    }

    public void Fechar()
    {
        fundoTransparente.SetActive(false);
        painelMenu.SetActive(false);
    }
}
