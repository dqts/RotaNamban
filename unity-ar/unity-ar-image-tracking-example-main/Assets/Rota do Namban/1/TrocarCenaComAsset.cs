using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation; // Adicionado para limpar o AR


#if UNITY_EDITOR
using UnityEditor;
#endif

public class TrocarCenaComAsset : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset cenaParaCarregar;
#endif

    [SerializeField] private string nomeCena = "";
    [SerializeField] private AudioSource audioSource; 

    [ContextMenu("Trocar Cena")]
    public void TrocarCena()
    {
        if (!string.IsNullOrEmpty(nomeCena))
        {
            StartCoroutine(TocarSomETrocarCena());
        }
        else
        {
            Debug.LogWarning("Nome da cena está vazio!");
        }
    }

    private System.Collections.IEnumerator TocarSomETrocarCena()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
LoaderUtility.Deinitialize();
LoaderUtility.Initialize();
        SceneManager.LoadScene(nomeCena);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (cenaParaCarregar != null)
        {
            string path = AssetDatabase.GetAssetPath(cenaParaCarregar);
            nomeCena = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
#endif
}

