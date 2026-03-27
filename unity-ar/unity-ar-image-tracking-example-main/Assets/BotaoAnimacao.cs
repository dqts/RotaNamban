using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoAnimacao : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 escalaOriginal;
    public float escalaAumentada = 1.1f;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal * escalaAumentada;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal;
    }
}
