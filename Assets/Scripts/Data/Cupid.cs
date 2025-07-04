using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Cupid")]
public class Cupid : Role
{
    public Cupid() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Ví dụ: chọn 2 người đầu tiên không phải mình
        var lovers = new List<PlayerController>();
        foreach (var p in players)
        {
            if (p != self && p.isAlive)
                lovers.Add(p);
            if (lovers.Count == 2) break;
        }
        if (lovers.Count == 2)
        {
            var action = new RoleActionData(RoleAction.ChooseLovers, self, lovers[0], lovers[1]);
            // TODO: Gửi action này cho ActionResolver xử lý
        }
    }
} 