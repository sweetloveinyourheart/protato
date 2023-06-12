using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoryManager : MonoBehaviour
{
    [SerializeField] MatchList matchList;
    [SerializeField] MatchDetail matchDetail;

    List<MatchListData> listData;

    public static HistoryManager Instance { get; private set; }

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

        GetMatchHistory();
    }

    public void GoBackToHome()
    {
        SceneManager.LoadScene("Home");
    }

    void GetMatchHistory()
    {
        StartCoroutine(ApiContext.Instance.Get("/match/get-history", HandleListResponse));
    }

    void HandleListResponse(ApiResponse res)
    {
        if (res.data != null)
        {
            string json = res.data.ToString();
            JSONArray jsonArray = JSON.Parse(json).AsArray;

            List<MatchListData> data = new List<MatchListData>();

            foreach (JSONNode node in jsonArray)
            {
                MatchListData matchData = new MatchListData();
                matchData._id = node["_id"];
                matchData.createdAt = node["createdAt"];
                matchData.victory = node["victory"].AsBool;

                JSONArray resultsArray = node["results"].AsArray;
                List<MatchListResult> results = new List<MatchListResult>();

                foreach (JSONNode resultNode in resultsArray)
                {
                    MatchListResult result = new MatchListResult();
                    result.player = resultNode["player"];
                    result.score = resultNode["score"].AsInt;

                    results.Add(result);
                }

                JSONArray playersArray = node["players"].AsArray;
                List<string> players = new List<string>();

                foreach (JSONNode playerNode in playersArray)
                {
                    players.Add(playerNode);
                }

                matchData.results = results;
                matchData.players = players;

                data.Add(matchData);
            }

            listData = data;
            ShowMatchList();
        }
    }

    public void ShowMatchDetail(string matchId)
    {
        matchList.ClearItems();
        matchList.gameObject.SetActive(false);

        matchDetail.gameObject.SetActive(true);
        StartCoroutine(ApiContext.Instance.Get("/match/get-by-id/" + matchId, HandleDetailResponse));
    }

    public void ShowMatchList()
    {
        matchDetail.ClearItems();
        matchDetail.gameObject.SetActive(false);

        matchList.gameObject.SetActive(true);
        matchList.ShowHistory(listData);
    }

    void HandleDetailResponse(ApiResponse res)
    {
        if (res.data != null)
        {
            string json = res.data.ToString();
            JSONNode matchJson = JSON.Parse(json);

            MatchData data = new MatchData();

            data._id = matchJson["_id"];
            data.createdAt = matchJson["createdAt"];
            data.victory = matchJson["victory"].AsBool;

            JSONArray resultsArray = matchJson["results"].AsArray;
            List<ResultData> results = new List<ResultData>();

            foreach (JSONNode resultNode in resultsArray)
            {
                ResultData result = new ResultData();

                JSONNode playerNode = resultNode["player"];
                PlayerData player = new PlayerData();
                player.username = playerNode["username"];
                player._id = playerNode["_id"];
                player.email = playerNode["email"];
                player.createdAt = playerNode["createdAt"];

                result.player = player;
                result.score = resultNode["score"].AsInt;

                results.Add(result);
            }

            JSONArray playersArray = matchJson["players"].AsArray;
            List<PlayerData> players = new List<PlayerData>();

            foreach (JSONNode playerNode in playersArray)
            {
                PlayerData player = new PlayerData();
                player.username = playerNode["username"];
                player._id = playerNode["_id"];
                player.email = playerNode["email"];
                player.createdAt = playerNode["createdAt"];

                players.Add(player);
            }

            data.results = results;
            data.players = players;


            matchDetail.ShowDetail(data);
        }
    }
}
