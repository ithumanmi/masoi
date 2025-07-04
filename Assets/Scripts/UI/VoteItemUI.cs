using UnityEngine;
using UnityEngine.UI;
using System;

public class VoteItemUI : MonoBehaviour
{
    public Text nameText;
    public Button voteButton;
    public PlayerController player;

    private bool isVoted = false;

    public void SetInfo(PlayerController player, Action<PlayerController> onVote)
    {
        this.player = player;
        nameText.text = player.playerName;
        isVoted = false;
        SetButtonColor(Color.white);

        voteButton.onClick.RemoveAllListeners();
        voteButton.onClick.AddListener(() =>
        {
            isVoted = !isVoted;
            SetButtonColor(isVoted ? Color.red : Color.white);
            onVote?.Invoke(player);
        });
    }

    private void SetButtonColor(Color color)
    {
        var colors = voteButton.colors;
        colors.normalColor = color;
        colors.selectedColor = color;
        colors.highlightedColor = color;
        voteButton.colors = colors;
    }

    public void SetVoted(bool voted)
    {
        isVoted = voted;
        SetButtonColor(isVoted ? Color.red : Color.white);
    }
} 