using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Lover")]
public class Lover : Role
{
    public Lover() { activityTime = RoleActivity.Both; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Nếu một người chết, người còn lại cũng chết (giả lập)
        // TODO: Logic này thường xử lý ở ActionResolver
    }
} 