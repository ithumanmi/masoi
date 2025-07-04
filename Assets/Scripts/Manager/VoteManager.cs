using System.Collections.Generic;
using UnityEngine;

public class VoteManager : MonoBehaviour
{
    public static VoteManager Instance { get; private set; }
    public Dictionary<PlayerController, int> voteCounts = new Dictionary<PlayerController, int>();

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

    public void ResetVotes(List<PlayerController> players)
    {
        voteCounts.Clear();
        foreach (var p in players)
        {
            voteCounts[p] = 0;
        }
    }

    public void AddVote(PlayerController target)
    {
        if (voteCounts.ContainsKey(target))
            voteCounts[target]++;
    }

    public PlayerController GetMostVoted()
    {
        PlayerController mostVoted = null;
        int maxVotes = -1;
        foreach (var kvp in voteCounts)
        {
            if (kvp.Value > maxVotes)
            {
                maxVotes = kvp.Value;
                mostVoted = kvp.Key;
            }
        }
        return mostVoted;
    }
}
