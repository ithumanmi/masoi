using System.Collections.Generic;
using UnityEngine;

public class ActionResolver : MonoBehaviour
{
    public static ActionResolver Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Danh sách action thu thập từ các vai trò trong đêm
    public List<RoleActionData> nightActions = new List<RoleActionData>();
    public List<RoleActionData> lastNightActions = new List<RoleActionData>();
    public IReadOnlyList<RoleActionData> LastNightActions => lastNightActions.AsReadOnly();

    // Thu thập action từ UI hoặc vai trò
    public void CollectAction(RoleActionData actionData)
    {
        if (actionData != null)
            nightActions.Add(actionData);
    }

    public void AddNightAction(RoleActionData action)
    {
        nightActions.Add(action);
    }

    public IReadOnlyList<RoleActionData> GetLastNightActions()
    {
        return lastNightActions.AsReadOnly();
    }

    public void ResolveNightActions(List<PlayerController> players)
    {
        // Lưu lại danh sách action đêm vừa qua để truy xuất kết quả đặc biệt
        lastNightActions.Clear();
        lastNightActions.AddRange(nightActions);

        // 1. Xác định các action đặc biệt: Protect, Save, Poison, Kill, v.v.
        PlayerController protectedPlayer = null;
        PlayerController savedPlayer = null;
        List<PlayerController> poisonedPlayers = new List<PlayerController>();
        List<PlayerController> killedByWerewolf = new List<PlayerController>();
        List<PlayerController> killedByHunter = new List<PlayerController>();
        List<PlayerController> lovers = new List<PlayerController>();

        // Xử lý các action
        foreach (var action in nightActions)
        {
            switch (action.action)
            {
                case RoleAction.Protect:
                    protectedPlayer = action.target;
                    break;
                case RoleAction.Save:
                    savedPlayer = action.target;
                    break;
                case RoleAction.Poison:
                    poisonedPlayers.Add(action.target);
                    break;
                case RoleAction.Kill:
                    if (action.source.role is Werewolf)
                        killedByWerewolf.Add(action.target);
                    else if (action.source.role is Hunter)
                        killedByHunter.Add(action.target);
                    break;
                case RoleAction.ChooseLovers:
                    lovers.Add(action.target);
                    if (action.extraData is PlayerController otherLover)
                        lovers.Add(otherLover);
                    break;
                // Các action khác như Inspect, Spy có thể xử lý riêng
            }
        }

        // 2. Ưu tiên bảo vệ, phù thủy cứu trước khi xử lý sói cắn
        foreach (var victim in killedByWerewolf)
        {
            if (victim == protectedPlayer || victim == savedPlayer)
            {
                // Được bảo vệ hoặc cứu, không chết
                continue;
            }
            victim.isAlive = false;
            // Nếu là Lover, người còn lại cũng chết
            if (lovers.Contains(victim))
            {
                foreach (var lover in lovers)
                {
                    lover.isAlive = false;
                }
            }
        }

        // 3. Xử lý bị đầu độc bởi phù thủy
        foreach (var victim in poisonedPlayers)
        {
            victim.isAlive = false;
            if (lovers.Contains(victim))
            {
                foreach (var lover in lovers)
                {
                    lover.isAlive = false;
                }
            }
        }

        // 4. Xử lý bị bắn bởi thợ săn
        foreach (var victim in killedByHunter)
        {
            victim.isAlive = false;
            if (lovers.Contains(victim))
            {
                foreach (var lover in lovers)
                {
                    lover.isAlive = false;
                }
            }
        }

        // 5. Xử lý các action khác (Inspect, Spy...)
        // TODO: Trả về kết quả cho Seer, LittleGirl, v.v.

        // 6. Reset action cho đêm tiếp theo
        nightActions.Clear();
    }

    public void ResolveVoteResults()
    {
        Debug.Log("ResolveVoteResults " + nightActions.Count);
        foreach (var action in nightActions)
        {

            if (action.action == RoleAction.VoteKick && action.source != null && action.target != null)
            {
                int damage = action.source.Damage;
                action.target.CurrentHP -= damage;
                Debug.Log($"Player {action.source.playerName} voted {action.target.playerName}, damage: {damage}, HP left: {action.target.CurrentHP}");
                if (action.target.CurrentHP <= 0)
                {
                    action.target.isAlive = false;
                    Debug.Log($"Player {action.target.playerName} died!");
                }
            }
        }
        nightActions.Clear();
    }
} 