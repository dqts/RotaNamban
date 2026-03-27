using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    [System.Serializable]
    public class Sticker
    {
        public Button button;               // Botão do autocolante
        public GameObject lockedImage;      // Imagem bloqueada
        public GameObject unlockedImage;    // Imagem desbloqueada
        public bool isUnlocked;             // Estado atual
        public GameObject popupCanvas;      // Canvas de popup
    }

    public Sticker[] stickers;

    [Header("Popup para quando o autocolante está bloqueado")]
    public GameObject popupOops;

    void Start()
    {
        // Carrega estados guardados
        for (int i = 0; i < stickers.Length; i++)
        {
            stickers[i].isUnlocked = PlayerPrefs.GetInt("Sticker_" + i, 0) == 1;
        }

        // Aplica desbloqueios da sessão atual (se houver)
        foreach (int index in StickerSessionData.stickersParaDesbloquear)
        {
            UnlockSticker(index);
        }

        // Atualiza UI
        UpdateStickersUI();

        // Limpa desbloqueios da sessão
        StickerSessionData.stickersParaDesbloquear.Clear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Atualização manual chamada.");
            UpdateStickersUI();
        }
    }

    public void UnlockSticker(int index)
    {
        if (index >= 0 && index < stickers.Length)
        {
            stickers[index].isUnlocked = true;
            SaveUnlockedStickers();
            UpdateStickersUI();
        }
    }

    void UpdateStickersUI()
    {
        for (int i = 0; i < stickers.Length; i++)
        {
            Sticker sticker = stickers[i];

            if (sticker.popupCanvas != null)
                sticker.popupCanvas.SetActive(false);

            if (sticker.lockedImage != null)
                sticker.lockedImage.SetActive(!sticker.isUnlocked);

            if (sticker.unlockedImage != null)
                sticker.unlockedImage.SetActive(sticker.isUnlocked);

            if (sticker.button != null)
                sticker.button.onClick.RemoveAllListeners();

            if (sticker.isUnlocked)
            {
                sticker.button.onClick.AddListener(() =>
                {
                    if (sticker.popupCanvas != null)
                        sticker.popupCanvas.SetActive(true);
                });
            }
            else
            {
                sticker.button.onClick.AddListener(() =>
                {
                    Debug.Log("Autocolante bloqueado.");
                    if (popupOops != null)
                        popupOops.SetActive(true);
                });
            }
        }
    }

    void SaveUnlockedStickers()
    {
        for (int i = 0; i < stickers.Length; i++)
        {
            PlayerPrefs.SetInt("Sticker_" + i, stickers[i].isUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void ClosePopup(GameObject popup)
    {
        if (popup != null)
            popup.SetActive(false);
    }
}
