using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using SimpleJSON;
using System.Text;

public class Register : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleEmailEdit(string str)
    {
        email = str;
    }

    public void HandlePasswordEdit(string str)
    {
        password = str;
    }

    public void CreateAccount()
    {
        RegisterRq();
    }

    void RegisterRq()
    {
        form.SetActive(false);
        mainMenu.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["password"] = password;

        ApiResponse res = ApiContext.Instance.Post("/auth/register", data);

        form.SetActive(true);
        mainMenu.ShowLoading(false);

        if (res.data != null)
        {
            mainMenu.ShowVerifyForm(email);
        }
        else
        {
            string errorMessage = res.error;
            mainMenu.ShowErrorPopup(errorMessage, "Register");
        }

    }
}
