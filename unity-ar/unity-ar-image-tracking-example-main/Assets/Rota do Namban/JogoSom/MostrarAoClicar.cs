using UnityEngine;
using UnityEngine.UI;

public class MostrarGameObjectAoClicar : MonoBehaviour
{
    [Header("Botão que será clicado")]
    public Button botao;

    [Header("Objeto a ser mostrado")]
    public GameObject objetoParaMostrar;

    void Start()
    {
        if (botao != null && objetoParaMostrar != null)
        {
            botao.onClick.AddListener(MostrarObjeto);
        }
        else
        {
            Debug.LogWarning("Botão ou Objeto para mostrar não foi atribuído.");
        }
    }

    void MostrarObjeto()
    {
        objetoParaMostrar.SetActive(true);
    }
}
