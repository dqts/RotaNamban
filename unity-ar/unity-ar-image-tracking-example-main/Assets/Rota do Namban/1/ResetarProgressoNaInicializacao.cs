using UnityEngine;

public class ResetarProgressoNaInicializacao : MonoBehaviour
{
    void Awake()
    {
        // PlayerPrefs normais
        PlayerPrefs.DeleteKey("CasacoFoiAbanado");
        PlayerPrefs.DeleteKey("Desbloqueado_Casaco");
        PlayerPrefs.DeleteKey("Desbloqueado_Espada");
        PlayerPrefs.DeleteKey("Desbloqueado_Documento");
        PlayerPrefs.DeleteKey("Desbloqueado_Saque");
        PlayerPrefs.DeleteKey("Desbloqueado_Bule");
        PlayerPrefs.DeleteKey("Desbloqueado_Arca");

        // Corrigido: Sticker keys (usando o nome correto "Sticker_")
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.DeleteKey("Sticker_" + i);
        }

        // Limpa desbloqueios da sessão
        if (StickerSessionData.stickersParaDesbloquear != null)
            StickerSessionData.stickersParaDesbloquear.Clear();

        PlayerPrefs.Save();
        Debug.Log("🔄 PlayerPrefs e dados de stickers resetados ao iniciar a cena.");
    }
}
