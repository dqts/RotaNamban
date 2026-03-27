using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BeadController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Ordem e Grupo")]
    public int index;
    public BeadController[] group;

    [Header("Degraus de Movimento (de baixo para cima)")]
    public List<float> positionsY = new List<float>();

    public int currentStep = 0;

    [Header("Som")]
    [SerializeField] private AudioSource audioSource;

    private Vector2 originalPos;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (positionsY.Count > 0)
        {
            MoveTo(positionsY[currentStep]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TentarMoverAutomaticamente();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out delta
        );

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, delta.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Encontra o degrau mais próximo
        float currentY = rectTransform.anchoredPosition.y;
        float maisProximo = positionsY[0];
        int degrauMaisProximo = 0;

        for (int i = 1; i < positionsY.Count; i++)
        {
            if (Mathf.Abs(currentY - positionsY[i]) < Mathf.Abs(currentY - maisProximo))
            {
                maisProximo = positionsY[i];
                degrauMaisProximo = i;
            }
        }

        if (!PosicaoOcupadaPorOutro(maisProximo))
        {
            currentStep = degrauMaisProximo;
            MoveTo(maisProximo);
        }
        else
        {
            // Volta à posição original se posição estiver ocupada
            MoveTo(positionsY[currentStep]);
        }
    }

    private void TentarMoverAutomaticamente()
    {
        if (positionsY == null || positionsY.Count == 0) return;

        if (currentStep < positionsY.Count - 1)
        {
            int upwardStep = currentStep + 1;
            float upwardY = positionsY[upwardStep];

            if (!PosicaoOcupadaPorOutro(upwardY))
            {
                currentStep = upwardStep;
                MoveTo(upwardY);
                return;
            }
            else if (currentStep > 0)
            {
                int downwardStep = currentStep - 1;
                float downwardY = positionsY[downwardStep];

                if (!PosicaoOcupadaPorOutro(downwardY))
                {
                    currentStep = downwardStep;
                    MoveTo(downwardY);
                    return;
                }
            }
        }
        else if (currentStep > 0)
        {
            int downwardStep = currentStep - 1;
            float downwardY = positionsY[downwardStep];

            if (!PosicaoOcupadaPorOutro(downwardY))
            {
                currentStep = downwardStep;
                MoveTo(downwardY);
                return;
            }
        }
    }

    public void MoveTo(float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public float GetY()
    {
        return rectTransform.anchoredPosition.y;
    }

    private bool PosicaoOcupadaPorOutro(float yDestino)
    {
        foreach (var other in group)
        {
            if (other != this && Mathf.Abs(other.GetY() - yDestino) < 0.1f)
            {
                return true;
            }
        }
        return false;
    }
}
