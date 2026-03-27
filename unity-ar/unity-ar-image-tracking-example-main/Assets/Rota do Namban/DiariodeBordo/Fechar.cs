using UnityEngine;

public class Fechar : MonoBehaviour
{
    public GameObject objectToShow;

    public void Show()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }
}
