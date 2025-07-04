using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }

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

    public void Connect()
    {
        // TODO: Kết nối server (dummy)
    }

    public void SendAction(string action, object data)
    {
        // TODO: Gửi action lên server (dummy)
    }

    public void OnReceiveAction(string action, object data)
    {
        // TODO: Xử lý action từ server (dummy)
    }
} 