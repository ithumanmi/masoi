using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ActionPanel : MonoBehaviour
{
    public Transform content; // Kéo thả Content của ScrollView/List vào đây
    public ActionItemUI actionItemPrefab; // Prefab cho từng dòng action
    public float timeoutSeconds = 10f; // Thời gian chờ chọn target

    private PlayerController currentPlayer;
    private List<PlayerController> currentTargets = new List<PlayerController>();
    private string currentActionName;
    private Coroutine timeoutCoroutine;

    public void ShowActions(PlayerController player, List<PlayerController> targets, string actionName)
    {
        gameObject.SetActive(true);
        currentPlayer = player;
        currentTargets = new List<PlayerController>(targets);
        currentActionName = actionName;
        foreach (Transform child in content)
            Destroy(child.gameObject);
        foreach (var target in targets)
        {
            if (!target.isAlive || target == player) continue;
            var item = Instantiate(actionItemPrefab, content);
            item.SetInfo(target.playerName, () => OnAction(player, target, actionName));
        }
        if (timeoutCoroutine != null) StopCoroutine(timeoutCoroutine);
        timeoutCoroutine = StartCoroutine(TimeoutSelect());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (timeoutCoroutine != null) StopCoroutine(timeoutCoroutine);
    }

    private void OnAction(PlayerController player, PlayerController target, string actionName)
    {
        RoleAction action = RoleAction.None;
        switch (actionName)
        {
            case "Kill": action = RoleAction.Kill; break;
            case "Protect": action = RoleAction.Protect; break;
            case "Inspect": action = RoleAction.Inspect; break;
            case "Save": action = RoleAction.Save; break;
            case "Poison": action = RoleAction.Poison; break;
            case "VoteKick": action = RoleAction.VoteKick; break;
            case "VoteSave": action = RoleAction.VoteSave; break;
            case "VoteSkip": action = RoleAction.VoteSkip; break;
        }
        if (action != RoleAction.None)
        {
            var actionData = new RoleActionData(action, player, target);
            ActionResolver.Instance.CollectAction(actionData);
        }
        Hide();
    }

    private IEnumerator TimeoutSelect()
    {
        float t = 0;
        while (t < timeoutSeconds)
        {
            t += Time.deltaTime;
            yield return null;
        }
        if (gameObject.activeSelf && currentTargets.Count > 0)
        {
            var possibleTargets = currentTargets.FindAll(p => p.isAlive && p != currentPlayer);
            if (possibleTargets.Count > 0)
            {
                var target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                OnAction(currentPlayer, target, currentActionName);
            }
            else
            {
                Hide();
            }
        }
    }
} 