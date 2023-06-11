using UnityEngine;
using SimpleJSON;
using System.Text;
using UnityEngine.Networking;

public class ApiResponse
{
    public JSONNode data;
    public string error;

    public ApiResponse(JSONNode jsData)
    {
        data = jsData;
    }

    public ApiResponse(string err)
    {
        error = err;
    }
}

public class ApiContext : MonoBehaviour
{
    public string BaseUrl = "http://localhost:6789";
    public string accessToken;
    public ProfileData user;

    public static ApiContext Instance { get; private set; }

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

    public ApiResponse Get(string route)
    {
        string url = BaseUrl + route;

        // Create a UnityWebRequest object with the GET method
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + accessToken);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request and wait for a response
        UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
        while (!asyncOperation.isDone) { }

        // Check if there were any errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            // If there was an error, parse the response data to get the error message
            JSONNode data = JSON.Parse(request.downloadHandler.text);
            string errorMessage = data["message"] ? data["message"] : "An error occurred !";

            return new ApiResponse(errorMessage);
        }
        else
        {
            JSONNode data = JSON.Parse(request.downloadHandler.text);
            // Request was successful
            return new ApiResponse(data);
        }
    }

    public ApiResponse Post(string route, JSONNode body)
    {
        string url = BaseUrl + route;

        string jsonString = body.ToString();
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

        // Create a UnityWebRequest object with the POST method
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();


        // Send the request and wait for a response
        UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
        while (!asyncOperation.isDone) { }

        // Check if there were any errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            // If there was an error, parse the response data to get the error message
            JSONNode data = JSON.Parse(request.downloadHandler.text);
            string errorMessage = data["message"] ? data["message"] : "An error occurred !";

            return new ApiResponse(errorMessage);
        }
        else
        {
            JSONNode data = JSON.Parse(request.downloadHandler.text);
            // Request was successful
            return new ApiResponse(data);
        }
    }
}
