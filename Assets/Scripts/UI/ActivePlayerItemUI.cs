using UnityEngine;
using UnityEngine.UI;
using System;

public class ActivePlayerItemUI : MonoBehaviour
{
    public Text nameText;
    public Button selectButton;
    private PlayerController player;
    private System.Action<PlayerController> onSelect;

    public void SetInfo(PlayerController player, System.Action<PlayerController> onSelect)
    {
        this.player = player;
        this.onSelect = onSelect;
        nameText.text = player.playerName;
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => this.onSelect?.Invoke(this.player));
    }
} 