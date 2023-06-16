using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using SimpleJSON;
using System.Threading.Tasks;

[Serializable]
class ClientEvent
{
    readonly public static string FindMatch = "FindMatch";
    readonly public static string LeaveQueue = "LeaveQueue";
    readonly public static string InitMatch = "InitMatch";
    readonly public static string UpdatePlayerState = "UpdatePlayerState";
    readonly public static string ApplicationQuit = "ApplicationQuit";
}

[Serializable]
class ServerEvent
{
    readonly public static string MatchJoined = "MatchJoined";
    readonly public static string MatchCreated = "MatchCreated";
    readonly public static string PlayerStateUpdated = "PlayerStateUpdated";
    readonly public static string UserLeft = "UserLeft";
}

public class SocketContext : MonoBehaviour
{
    public string serverIP = "127.0.0.1";
    public int serverPort = 3333;

    [HideInInspector] public UdpClient udpClient;

    public static SocketContext Instance { get; private set; }

    private void Start()
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

    public void ConnectToServer()
    {
        udpClient = new UdpClient();
        udpClient.Connect(serverIP, serverPort);
    }

    public void DisconnectToServer()
    {
        udpClient = null;
        udpClient?.Close();
    }

    public void SendMessage(string eventType, JSONObject data)
    {
        if (udpClient != null)
        {
            // Create a JSON object with matchId and playerData
            JSONObject payload = new JSONObject();
            payload["type"] = eventType;
            payload["data"] = data;

            string json = payload.ToString();

            // Convert the JSON to bytes and send it to the server
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
            udpClient.Send(buffer, buffer.Length);
        }
    }

    public async Task<JSONNode> ReceiveMessageAsync()
    {
        if (udpClient != null)
        {
            try
            {
                var x = await udpClient.ReceiveAsync();
                byte[] receiveBytes = x.Buffer;
                string returnData = Encoding.ASCII.GetString(receiveBytes);

                //Debug.Log("This is the message you received: " + returnData);
                return JSON.Parse(returnData);
            }
            catch
            {
                return null;
            }
        }

        return null;
    }

    private void OnApplicationQuit()
    {
        if(MatchManager.Instance != null)
        {
            JSONObject payload = new JSONObject();
            payload["matchId"] = MatchManager.Instance.matchId;
            payload["userId"] = ApiContext.Instance.user._id;

            SendMessage(ClientEvent.ApplicationQuit, payload);
        }

        if(ApiContext.Instance != null)
        {
            if (ApiContext.Instance.user != null)
            {
                StartCoroutine(ApiContext.Instance.Delete("/auth/logout", (res) => { }));
            }
        }

        udpClient?.Close();
    }
}
