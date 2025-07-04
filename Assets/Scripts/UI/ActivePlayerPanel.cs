using UnityEngine;
using System.Collections.Generic;

public class ActivePlayerPanel : MonoBehaviour
{
    public Transform content;
    public ActivePlayerItemUI activePlayerItemPrefab;

    // Callback khi chọn player (ví dụ: hiện panel vote)
    public System.Action<PlayerController> onPlayerSelected;

    public void ShowActivePlayers(List<PlayerController> players)
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (var player in players)
        {
            var itemUI = Instantiate(activePlayerItemPrefab, content);
            itemUI.SetInfo(player, OnSelectPlayer);
        }
    }

    private void OnSelectPlayer(PlayerController player)
    {
        onPlayerSelected?.Invoke(player);
    }
} 