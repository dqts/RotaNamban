using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrocaDiario : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Arraste a cena aqui")]
    public SceneAsset cenaDestinoAsset;
#endif

    [HideInInspector]
    public string nomeCenaDestino;

    private void Awake()
    {
#if UNITY_EDITOR
        if (cenaDestinoAsset != null)
        {
            nomeCenaDestino = cenaDestinoAsset.name;
        }
#endif
    }

    public void TrocarCena()
    {
        if (!string.IsNullOrEmpty(nomeCenaDestino))
        {
            SceneManager.LoadScene(nomeCenaDestino);
        }
        else
        {
            Debug.LogWarning("Nome da cena de destino não definido.");
        }
    }
}
