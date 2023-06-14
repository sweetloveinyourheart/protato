using UnityEngine;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class FindMatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JSONObject jsonObj = new JSONObject();
        jsonObj["userId"] = ApiContext.Instance.user._id;

        SocketContext.Instance.SendMessage(ClientEvent.FindMatch, jsonObj);
    }

    private async void FixedUpdate()
    {
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();
        JSONNode data = res["data"];

        string matchId = (data != null) ? data["matchId"] : null;
        if(matchId != null)
        {
            MatchManager.Instance.matchId = matchId;
            SceneManager.LoadScene("Multiplayer");
        }
    }
}
