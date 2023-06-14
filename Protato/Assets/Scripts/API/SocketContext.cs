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
    readonly public static string Move = "Move";
    readonly public static string Attack = "Attack";
}

[Serializable]
class ServerEvent
{
    readonly public static string MatchJoined = "MatchJoined";
    readonly public static string MatchCreated = "MatchCreated";
    readonly public static string PlayerMoved = "PlayerMoved";
}

public class SocketContext : MonoBehaviour
{
    public string serverIP = "127.0.0.1";
    public int serverPort = 3333;

    [HideInInspector] public UdpClient udpClient;
    [HideInInspector] public IPEndPoint remoteIpEndPoint;

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
        
        remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        udpClient = new UdpClient();
        udpClient.Connect(serverIP, serverPort);

        // Ensure the instance persists between scene loads
        DontDestroyOnLoad(gameObject);
    }

    public async Task<bool> SendMessageAsync(string eventType, JSONObject data)
    {
        // Create a JSON object with matchId and playerData
        JSONObject payload = new JSONObject();
        payload["type"] = eventType;
        payload["data"] = data;

        string json = payload.ToString();

        // Convert the JSON to bytes and send it to the server
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);
        await udpClient.SendAsync(buffer, buffer.Length);
        return true;
    }

    public void SendMessage(string eventType, JSONObject data)
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

    public async Task<JSONNode> ReceiveMessageAsync()
    {
        try
        {
            var x = await udpClient.ReceiveAsync();
            byte[] receiveBytes = x.Buffer;
            string returnData = Encoding.ASCII.GetString(receiveBytes);

            //Debug.Log("This is the message you received: " + returnData);
            return JSON.Parse(returnData); ;
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return null;
        }
    }
}
