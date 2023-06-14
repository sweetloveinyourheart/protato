using SimpleJSON;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameResult gameResult;
    [SerializeField] Ally ally;
    [SerializeField] Player player;

    private int activeMonsters; // Track the number of active monsters
    private int score = 0; // 1 monster die equal to 1 point
    float spawnDelay = 5f;
    int numberOfMonsters = 10;

    private bool isInit = false;

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

    private async void FixedUpdate()
    {
        if (!isInit) InitMatch();

        // listen to server message
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();

        string type = res["type"];
        JSONNode data = res["data"];

        if (type == ServerEvent.MatchCreated)
        {
            isInit = true;
            ApplyInitPostData(data);
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
        JSONArray players = matchData["playersPos"].AsArray;

        foreach (JSONNode p in players)
        {
            bool isPlayerPos = ApiContext.Instance.user._id == p["userId"];
            if (isPlayerPos)
            {
                player.gameObject.SetActive(true);
                player.userId = p["userId"];
                player.InitPos(p["xPos"].AsInt, p["yPos"].AsInt);
            }
            else
            {
                ally.gameObject.SetActive(true);
                ally.userId = p["userId"];
                ally.InitPos(p["xPos"].AsInt, p["yPos"].AsInt);
            }
        }
    }
}
