using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerItemUI : MonoBehaviour
{
    public Text nameText;
    //public Text roleText;
    public Image statusIcon; // Xanh: sống, Đỏ: chết
    public Text voteCountText;

    public void SetInfo(PlayerController player, int voteCount = 0)
    {
        nameText.text = player.playerName; //player.name;
        //roleText.text = role ?? "";
        statusIcon.color = player.isAlive ? Color.green : Color.red;
        if (voteCountText != null)
            voteCountText.text = voteCount > 0 ? voteCount.ToString() : "";

    }

    public void ClearVoteCount()
    {
        if (voteCountText != null)
            voteCountText.text = string.Empty;
    }
} 