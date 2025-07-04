using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    public Text popupText; // Kéo thả Text UI vào đây

    public void Show(string message)
    {
        popupText.text = message;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
} 