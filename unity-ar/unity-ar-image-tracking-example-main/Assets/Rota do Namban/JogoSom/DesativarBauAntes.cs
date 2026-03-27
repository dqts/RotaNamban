using UnityEngine;

public class DesativarBauAntes : MonoBehaviour
{
    public GameObject alvo;

    public void Desativar()
    {
        if (alvo != null)
        {
            alvo.SetActive(false);
        }
    }
}
