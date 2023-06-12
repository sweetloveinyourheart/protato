using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using SimpleJSON;

[System.Serializable]
public class ProfileData
{
    public string _id;
    public string email;
    public string username;
    public string createdAt;
    public int __v;
}

public class HomeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameText;
    // Start is called before the first frame update
    void Start()
    {
        GetProfile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetProfile()
    {
        string route = "/user/profile";
        StartCoroutine(ApiContext.Instance.Get(route, HandleProfileResponse));

    }

    void HandleProfileResponse(ApiResponse res)
    {
        if (res.data != null)
        {
            ProfileData userProfile = JsonUtility.FromJson<ProfileData>(res.data.ToString());
            ApiContext.Instance.user = userProfile; // Store to API context instance
            usernameText.text = userProfile.username; // Show username
        }
    }

    public void EnterSinglePlayer()
    {
        string createMatchUrl = "/match/single-player/create";
        StartCoroutine(ApiContext.Instance.Post(createMatchUrl, HandleSingleMatchResponse));
    }

    void HandleSingleMatchResponse(ApiResponse res)
    {
        if(res.data != null)
        {
            string matchId = res.data["_id"];
            MatchManager.Instance.matchId = matchId;
            SceneManager.LoadScene("Game");
        }
    }
}
