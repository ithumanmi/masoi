using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Witch")]
public class Witch : Role
{
    public Witch() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Ví dụ: cứu người đầu tiên, giết người thứ hai nếu có
        if (players.Count > 0)
        {
            var saveAction = new RoleActionData(RoleAction.Save, self, players[0]);
            // TODO: Gửi saveAction cho ActionResolver xử lý
        }
        if (players.Count > 1)
        {
            var poisonAction = new RoleActionData(RoleAction.Poison, self, players[1]);
            // TODO: Gửi poisonAction cho ActionResolver xử lý
        }
    }
} 