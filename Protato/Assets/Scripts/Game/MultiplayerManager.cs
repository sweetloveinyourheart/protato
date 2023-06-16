using SimpleJSON;
using UnityEngine;
using System.Collections;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] GameResult gameResult;
    [SerializeField] Enemy enemy;
    [SerializeField] Player player;

    private int score = 0; // win 100 - lose 0

    private bool isInit = false;
    private bool isFinish = false;

    public static MultiplayerManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        if(!isFinish)
        {
            SetupMatch();
        }
    }

    async void SetupMatch()
    {
        if (!isInit) InitMatch();

        // listen to server message
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();

        if (res != null)
        {
            string type = res["type"];
            JSONNode data = res["data"];

            if (type == ServerEvent.MatchCreated && !isInit)
            {
                ApplyInitPostData(data);
            }

            if (type == ServerEvent.UserLeft && !isFinish)
            {
                EnemyDead();
            }
        }
    }

    void InitMatch()
    {
        // Send a UDP message for init match
        JSONObject jsonObj = new JSONObject();
        jsonObj["matchId"] = MatchManager.Instance.matchId;

        SocketContext.Instance.SendMessage(ClientEvent.InitMatch, jsonObj);
    }

    private void ApplyInitPostData(JSONNode matchData)
    {
        // Setup character
        JSONArray players = matchData["playersPos"].AsArray;

        foreach (JSONNode p in players)
        {
            bool isPlayer = ApiContext.Instance.user._id == p["userId"];
            if (isPlayer)
            {
                player.gameObject.SetActive(true);
                player.userId = p["userId"];
                player.InitPos(p["xPos"].AsInt, p["yPos"].AsInt);
            }
            else
            {
                enemy.gameObject.SetActive(true);
                enemy.userId = p["userId"];
                enemy.InitPos(p["xPos"].AsInt, p["yPos"].AsInt);
            }
        }

        isInit = true;
    }

    public void PlayerDead()
    {
        score = 0;

        MatchManager.Instance.SaveResult(false, score);
        StartCoroutine(ShowResultCoroutine(false));

        isFinish = true;
    }

    public void EnemyDead()
    {
        score = 100;

        MatchManager.Instance.SaveResult(true, score);
        StartCoroutine(ShowResultCoroutine(true));

        isFinish = true;
    }

    private IEnumerator ShowResultCoroutine(bool isWinner)
    {
        yield return new WaitForSeconds(1f);

        gameResult.Show(isWinner, score);
        gameResult.gameObject.SetActive(true);
        SocketContext.Instance.DisconnectToServer();
    }
}
