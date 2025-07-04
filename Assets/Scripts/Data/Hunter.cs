using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Hunter")]
public class Hunter : Role
{
    public Hunter() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Nếu bị loại, chọn một người để bắn
        if (!self.isAlive)
        {
            PlayerController target = players.Find(p => p.isAlive && p != self);
            if (target != null)
            {
                var action = new RoleActionData(RoleAction.Kill, self, target);
                // TODO: Gửi action này cho ActionResolver xử lý
            }
        }
    }
} 