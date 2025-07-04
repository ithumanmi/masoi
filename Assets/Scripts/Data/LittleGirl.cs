using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/LittleGirl")]
public class LittleGirl : Role
{
    public LittleGirl() { activityTime = RoleActivity.Night; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Có thể lén xem sói chọn ai (giả lập)
        var action = new RoleActionData(RoleAction.Spy, self);
        // TODO: Gửi action này cho ActionResolver xử lý
    }
} 