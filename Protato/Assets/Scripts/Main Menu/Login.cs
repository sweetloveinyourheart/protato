using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] GameObject form;

    MainMenu mainMenu;
    string email = "";
    string password = "";

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();
    }

    public void OnLoginClick()
    {
        LoginRq();

    }

    public void HandleEmailEdit(string str)
    {
        email = str;
    }

    public void HandlePasswordEdit(string str)
    {
        password = str;
    }

    void LoginRq()
    {
        form.SetActive(false);
        mainMenu.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["password"] = password;

        ApiResponse res = ApiContext.Instance.Post("/auth/login", data);

        form.SetActive(true);
        mainMenu.ShowLoading(false);

        if (res.data != null)
        {
            string accessToken = res.data["accessToken"];
            ApiContext.Instance.accessToken = accessToken;
            SceneManager.LoadScene("Home");
        } else
        {
            string errorMessage = res.error;
            mainMenu.ShowErrorPopup(errorMessage, "Login");
        }

    }
}
