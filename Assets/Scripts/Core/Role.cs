using UnityEngine;
using System.Collections.Generic;

public enum RoleActivity
{
    Day,
    Night,
    Both
}

public abstract class Role : ScriptableObject
{
    public string roleName;
    public string description;
    public RoleActivity activityTime = RoleActivity.Night;
    public abstract void NightAction(PlayerController self, List<PlayerController> players);
} 