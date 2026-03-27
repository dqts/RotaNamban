using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropDetector : MonoBehaviour, IDropHandler
{
    [Header("Popup a mostrar se item for espada")]
    public GameObject popupEspada;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject itemArrastado = eventData.pointerDrag;

        if (itemArrastado != null && itemArrastado.name.ToLower().Contains("espada"))
        {
            Debug.Log("Espada foi arrastada e solta!");

            // Mostra popup se estiver definido
            if (popupEspada != null)
                popupEspada.SetActive(true);
                  PlayerPrefs.DeleteKey("CasacoFoiAbanado");
        PlayerPrefs.DeleteKey("Desbloqueado_Casaco");
        PlayerPrefs.DeleteKey("Desbloqueado_Espada");
        PlayerPrefs.DeleteKey("Desbloqueado_Documento");
        PlayerPrefs.DeleteKey("Desbloqueado_Saque");
        PlayerPrefs.DeleteKey("Desbloqueado_Bule");
        PlayerPrefs.DeleteKey("Desbloqueado_Arca");
        }
    }
}
