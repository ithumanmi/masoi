using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Roles/Villager")]
public class Villager : Role
{
    public Villager() { activityTime = RoleActivity.Day; }
    public override void NightAction(PlayerController self, List<PlayerController> players)
    {
        // Dân làng không có hành động ban đêm
    }
} 