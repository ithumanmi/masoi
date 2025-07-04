using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemUIWIthButton : MonoBehaviour
{

    public Text nameText;
    //public Text roleText;
    public Image statusIcon; // Xanh: sống, Đỏ: chết
    public Button selectButton;
    private Action onClickCallback;
    public PlayerController player;

    public void SetInfo(PlayerController player, Action<PlayerController> onClick)
    {
        nameText.text = player.playerName;
        //roleText.text = role ?? "";
        statusIcon.color = player.isAlive ? Color.yellow : Color.black;
        this.player = player;
        onClickCallback = () => onClick?.Invoke(player);
        if (selectButton != null)
        {
            selectButton.onClick.RemoveAllListeners();
            selectButton.onClick.AddListener(() => onClickCallback?.Invoke());
        }
    }
}
