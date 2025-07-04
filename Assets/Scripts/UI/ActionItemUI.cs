using UnityEngine;
using UnityEngine.UI;
using System;

public class ActionItemUI : MonoBehaviour
{
    public Text nameText;
    public Button actionButton;

    public void SetInfo(string name, Action onAction)
    {
        nameText.text = name;
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => onAction?.Invoke());
    }
} 