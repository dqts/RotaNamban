using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventarioAvanco : MonoBehaviour
{
    public InventarioManager inventarioManager;
    public GameObject imagemChave;
    public GameObject areaExibicao; // RawImage grande
    public string proximaCena;

    public void Avancar()
    {
        // Se a chave foi mostrada, então o casaco foi abanado
        bool casacoFoiInteragido = imagemChave != null && imagemChave.activeSelf;

        if (casacoFoiInteragido && inventarioManager != null)
        {
            // Reset: limpa itens desbloqueados
            inventarioManager.ResetarInventario();

            // Esconde chave
            if (imagemChave != null)
                imagemChave.SetActive(false);

            // Limpa imagem grande (opcional)
            if (areaExibicao != null)
            {
                var raw = areaExibicao.GetComponent<RawImage>();
                if (raw != null)
                {
                    raw.texture = null;
                    raw.color = new Color32(219, 171, 129, 255); // #DBAB81
                }
            }
        }

        // Avança de cena sempre
        SceneManager.LoadScene(proximaCena);
    }
}
