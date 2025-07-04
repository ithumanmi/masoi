using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance { get; private set; }
    public List<string> publicChat = new List<string>();
    public List<string> werewolfChat = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SendPublicMessage(string message)
    {
        publicChat.Add(message);
        // TODO: Cập nhật UI chat chung
    }

    public void SendWerewolfMessage(string message)
    {
        werewolfChat.Add(message);
        // TODO: Cập nhật UI chat sói
    }
} 