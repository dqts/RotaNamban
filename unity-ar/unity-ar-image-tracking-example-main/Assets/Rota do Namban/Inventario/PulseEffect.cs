using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;       // Velocidade da palpitação
    public float pulseAmount = 0.1f;    // Quanto o item aumenta/diminui de tamanho

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        float scaleModifier = 1 + Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);
        transform.localScale = initialScale * scaleModifier;
    }
}
