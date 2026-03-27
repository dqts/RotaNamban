using UnityEngine;

public class AlternarGameObjects : MonoBehaviour
{
    public GameObject esconderEste;
    public GameObject mostrarEste;

    public void Trocar()
    {
        if (esconderEste != null)
            esconderEste.SetActive(false);

        if (mostrarEste != null)
            mostrarEste.SetActive(true);
    }
}
