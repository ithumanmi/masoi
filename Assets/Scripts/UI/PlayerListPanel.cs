using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerListPanel : MonoBehaviour
{
    public Transform content; // Kéo thả Content của ScrollView/List vào đây
    public PlayerItemUI playerItemPrefab; // Prefab cho từng dòng player

    public void UpdateList(List<PlayerController> players)
    {
        if (content == null || playerItemPrefab == null)
        {
            Debug.LogError("PlayerListPanel: content hoặc playerItemPrefab chưa được gán!");
            return;
        }

        foreach (Transform child in content)
            Destroy(child.gameObject);
        
        foreach (var player in players)
        {
            int voteCount = 0;
            if (GameManager.Instance.voteCounts != null && GameManager.Instance.voteCounts.TryGetValue(player, out var count))
                voteCount = count;
            var itemUI = Instantiate(playerItemPrefab, content);
            itemUI.SetInfo(player, voteCount);
        }
    }
}