using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ItemVisualizador : MonoBehaviour
{
    [Header("Configuração")]
    public Texture imagemGrande;
    public RawImage areaExibicao;
    public bool desbloqueado = false;
[Header("Objetos a ocultar após a chave")]
public GameObject[] objetosParaOcultar;
[Header("Imagem final após entrega")]
public Texture imagemFinal; // arraste a imagem desejada no Inspector
[Header("Popup ao exibir este item")]
public GameObject popupAoMostrar;

    [Header("Interação Especial do Casaco")]
    public bool itemEspecialCasaco = false; // Marque isso no casaco
    public GameObject chave;
    public InventarioManager inventarioManager; // Referência ao inventário para contar itens

    private static readonly Color corPadrao = new Color32(219, 171, 129, 255);
    private static readonly Color corSelecionado = Color.white;

    private void Start()
    {
        GetComponent<Button>().interactable = desbloqueado;

        if (areaExibicao != null && areaExibicao.texture == null)
            areaExibicao.color = corPadrao;

        if (areaExibicao != null && itemEspecialCasaco)
        {
            // Adiciona detecção de clique na imagem grande
            Button btn = areaExibicao.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnImagemGrandeClicada);
            }
        }
if (itemEspecialCasaco && PlayerPrefs.GetInt("CasacoFoiAbanado", 0) == 1)
{
    // Já abanado antes: ocultar os objetos e mostrar imagem final direto
    foreach (var obj in objetosParaOcultar)
    {
        if (obj != null)
            obj.SetActive(false);
    }

    if (chave != null)
        chave.SetActive(false);

    if (areaExibicao != null && imagemFinal != null)
    {
        areaExibicao.texture = imagemFinal;
        areaExibicao.color = Color.white;
        areaExibicao.gameObject.SetActive(true);
    }

    // Também revela os últimos 3 itens, se for o caso
    SequenciaFinalItens sequencia = FindObjectOfType<SequenciaFinalItens>();
    if (sequencia != null)
    {
        sequencia.MostrarSegundaFase();
    }
}

        if (chave != null)
            chave.SetActive(false);
    }

  public void MostrarItem()
{
    if (!desbloqueado) return;

    if (areaExibicao != null && imagemGrande != null)
    {
        areaExibicao.texture = imagemGrande;
        areaExibicao.color = corSelecionado;
        areaExibicao.gameObject.SetActive(true);

        // Mostra popup, se definido e se for a espada
        if (popupAoMostrar != null && gameObject.name.ToLower().Contains("espada"))
        {
            popupAoMostrar.SetActive(true);
        }
    }
}


 public void Desbloquear()
{
    desbloqueado = true;
    GetComponent<Button>().interactable = true;
    gameObject.SetActive(true); // <- importante manter aqui
}


    public void LimparExibicao()
    {
        if (areaExibicao != null)
        {
            areaExibicao.texture = null;
            areaExibicao.color = corPadrao;
        }
    }

    void OnImagemGrandeClicada()
    {
        // Só faz animação se for o item especial (casaco) e pelo menos 3 itens desbloqueados
        if (itemEspecialCasaco && inventarioManager != null && inventarioManager.TotalItensDesbloqueados() >= 3)
        {
            StartCoroutine(AbanarCasacoERevelarChave());

#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }

   IEnumerator AbanarCasacoERevelarChave()
{
    Vector3 original = areaExibicao.rectTransform.localPosition;

    for (int i = 0; i < 6; i++)
    {
        areaExibicao.rectTransform.localPosition = original + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        yield return new WaitForSeconds(0.04f);
    }

    areaExibicao.rectTransform.localPosition = original;

    if (chave != null)
        chave.SetActive(true);

    yield return new WaitForSeconds(2f);

    if (chave != null)
        chave.SetActive(false);

    foreach (var obj in objetosParaOcultar)
    {
        if (obj != null)
            obj.SetActive(false);
    }

    // Mostra imagem final na área de exibição
    if (areaExibicao != null && imagemFinal != null)
    {
        areaExibicao.texture = imagemFinal;
        areaExibicao.color = Color.white;
        areaExibicao.gameObject.SetActive(true);
    }

    SequenciaFinalItens sequencia = FindObjectOfType<SequenciaFinalItens>();
if (sequencia != null)
{
    // Após abanar o casaco, revela os 3 itens finais
    sequencia.MostrarSegundaFase();
}
PlayerPrefs.SetInt("CasacoFoiAbanado", 1);
PlayerPrefs.Save();

}


}
