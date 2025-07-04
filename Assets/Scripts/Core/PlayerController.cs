using UnityEngine;
[System.Serializable]
public class PlayerController
{
    public string playerName;
    public Role role;
    public bool isAlive = true;
    public int CurrentHP = 10;
    public int CurrentMP = 10;
    public int Damage = 100;
} 