using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCenaComHistorico : MonoBehaviour
{
    public string nomeDaCenaDestino;
    [SerializeField] private AudioSource audioSource; // ← adiciona campo de som

    public void Trocar()
    {
        StartCoroutine(TocarSomETrocar());
    }

    private System.Collections.IEnumerator TocarSomETrocar()
    {
        // Toca o som (se houver)
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // Salva o nome da cena atual
        PlayerPrefs.SetString("CenaAnterior", SceneManager.GetActiveScene().name);

        // Carrega a próxima cena
        SceneManager.LoadScene(nomeDaCenaDestino);
    }
}