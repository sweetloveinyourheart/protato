using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MatchManager : MonoBehaviour
{
    [SerializeField] public string matchId;

    public static MatchManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ensure the instance persists between scene loads
        DontDestroyOnLoad(gameObject);
    }

    public void SaveResult(bool isWinner, int totalScore)
    {
        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["score"] = totalScore;
        data["victory"] = isWinner;

        StartCoroutine(ApiContext.Instance.Put("/match/save-result/" + matchId, data, HandleResponse));
    }

    void HandleResponse(ApiResponse res)
    {
    }
}
