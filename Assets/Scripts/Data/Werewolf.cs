using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Werewolf")]
public class Werewolf : Role
{
    public Werewolf() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Ví dụ: chọn nạn nhân đầu tiên không phải sói
        PlayerController target = players.Find(p => p != self && p.isAlive && !(p.role is Werewolf));
        if (target != null)
        {
            var action = new RoleActionData(RoleAction.Kill, self, target);
            // TODO: Gửi action này cho ActionResolver xử lý
        }
    }
} 