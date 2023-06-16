using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindMatch : MonoBehaviour
{
    private bool isWaitingForResponse = true;

    // Start is called before the first frame update
    void Start()
    {
        JSONObject jsonObj = new JSONObject();
        jsonObj["userId"] = ApiContext.Instance.user._id;

        SocketContext.Instance.ConnectToServer();
        SocketContext.Instance.SendMessage(ClientEvent.FindMatch, jsonObj);
    }

    private void FixedUpdate()
    {
        if (isWaitingForResponse)
        {
            CheckResponse();
        }
    }

    private async void CheckResponse()
    {
        JSONNode res = await SocketContext.Instance.ReceiveMessageAsync();
        if (res != null)
        {
            JSONNode data = res["data"];

            string matchId = (data != null) ? data["matchId"] : null;
            if (matchId != null)
            {
                MatchManager.Instance.matchId = matchId;
                isWaitingForResponse = false;
                SceneManager.LoadScene("Multiplayer");
            }

        }
    }
}
