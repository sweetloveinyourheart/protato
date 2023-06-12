using System.Collections;
using System.Security.Cryptography;
using System.Text;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] GameObject form;

    string email = "";
    string password = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnLoginClick()
    {
        form.SetActive(false);
        MainMenu.Instance.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["password"] = password;

        StartCoroutine(ApiContext.Instance.Post("/auth/login", data, HandleResponse));
    }

    public void HandleEmailEdit(string str)
    {
        email = str;
    }

    public void HandlePasswordEdit(string str)
    {
        password = str;
    }

    void HandleResponse(ApiResponse res)
    {
        MainMenu.Instance.ShowLoading(false);
        form.SetActive(true);

        if (res.data != null)
        {
            string accessToken = res.data["accessToken"];
            ApiContext.Instance.accessToken = accessToken;
            SceneManager.LoadScene("Home");
        }
        else
        {
            string errorMessage = res.error;
            MainMenu.Instance.ShowErrorPopup(errorMessage, "Login");
        }
    }
}
