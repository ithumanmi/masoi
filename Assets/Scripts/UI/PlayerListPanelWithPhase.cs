using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListPanelWithPhase : MonoBehaviour
{
    public Transform content; // Kéo thả Content của ScrollView/List vào đây
    public  PlayerItemUIWIthButton playerItemPrefab; // Prefab cho từng dòng player
    
    // Object pool cho PlayerItemUIWIthButton
    public List<PlayerItemUIWIthButton> itemPool = new List<PlayerItemUIWIthButton>();
  
    public void UpdateList(List<PlayerController> players, RoleActivity roleActivity, Action click = null)
    {
        Debug.Log("UpdateList");
        if (content == null || playerItemPrefab == null)
        {
            Debug.LogError("PlayerListPanel: content hoặc playerItemPrefab chưa được gán!");
            return;
        }

        int poolIndex = 0;

        foreach (var player in players)
        {
            if(player.role.activityTime == roleActivity && player.isAlive)
            {
                PlayerItemUIWIthButton itemUI;
                // Nếu pool chưa đủ, tạo mới
                if (poolIndex >= itemPool.Count)
                {
                    itemUI = Instantiate(playerItemPrefab, content);
                    itemPool.Add(itemUI);
                }
                else
                {
                    itemUI = itemPool[poolIndex];
                    itemUI.gameObject.SetActive(true);
                }
                // Truyền callback đúng kiểu Action<PlayerController>
                itemUI.SetInfo(player, (srcPlayer) => UIManager.Instance.ShowVotePanelExcludePlayer(srcPlayer));
                poolIndex++;
            }
        }

        // Ẩn các item dư thừa trong pool
        for (int i = poolIndex; i < itemPool.Count; i++)
        {
            itemPool[i].gameObject.SetActive(false);
        }
    }

    public void SetInteractable(bool interactable)
    {
        foreach (var item in itemPool)
        {
            if (item.gameObject.activeSelf && item.selectButton != null)
                item.selectButton.interactable = interactable;
        }
    }
}
