using UnityEngine;

public class Abrir : MonoBehaviour
{
    public GameObject objectToShow;
    public GameObject objectAtual;

    public void Show()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
            objectAtual.SetActive(false);
        }
    }
}
