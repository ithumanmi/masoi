using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Seer")]
public class Seer : Role
{
    public Seer() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Ví dụ: chọn người đầu tiên không phải mình
        PlayerController target = players.Find(p => p != self && p.isAlive);
        if (target != null)
        {
            var action = new RoleActionData(RoleAction.Inspect, self, target);
            // TODO: Gửi action này cho ActionResolver xử lý
        }
    }
} 