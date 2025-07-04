using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/OldMan")]
public class OldMan : Role
{
    public OldMan() { activityTime = RoleActivity.Both; }
    public int lives = 2; // Ví dụ: già làng có 2 mạng
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Không có action đêm, nhưng có thể kiểm tra số mạng sống
    }
} 