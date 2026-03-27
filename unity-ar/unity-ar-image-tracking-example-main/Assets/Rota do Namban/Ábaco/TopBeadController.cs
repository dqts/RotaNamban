using UnityEngine;
using UnityEngine.EventSystems;

public class TopBeadController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Posições")]
    public float posCima = 209.3f;
    public float posBaixo = 133.02f;

    private bool estaBaixo = false;

    [Header("Som")]
    [SerializeField] private AudioSource audioSource;

    private Vector2 dragStartPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        MoveTo(posCima);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AlternarPosicao();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        // Apenas arrasta verticalmente
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, localPoint.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float y = rectTransform.anchoredPosition.y;

        // Decide se vai para cima ou para baixo com base na posição final
        float meio = (posCima + posBaixo) / 2f;
        bool novoEstaBaixo = y < meio;

        if (novoEstaBaixo != estaBaixo)
        {
            estaBaixo = novoEstaBaixo;
            if (audioSource != null)
                audioSource.Play();
        }

        float destino = estaBaixo ? posBaixo : posCima;
        MoveTo(destino);
    }

    private void AlternarPosicao()
    {
        estaBaixo = !estaBaixo;
        float destino = estaBaixo ? posBaixo : posCima;
        MoveTo(destino);

        if (audioSource != null)
            audioSource.Play();
    }

    public bool EstaBaixo()
    {
        return estaBaixo;
    }

    private void MoveTo(float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
    }
}
