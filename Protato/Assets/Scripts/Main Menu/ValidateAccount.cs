using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class ValidateAccount : MonoBehaviour
{
    [SerializeField] GameObject form;

    string validationCode;
    string email;
    MainMenu mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleValidationCode(string str)
    {
        validationCode = str;
    }

    public void SetVerifyEmail(string mail)
    {
        email = mail;
    }

    public void Verify()
    {
        ValidateRq();
    }

    void ValidateRq()
    {
        form.SetActive(false);
        mainMenu.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["code"] = validationCode;

        ApiResponse res = ApiContext.Instance.Post("/auth/verify-account", data);

        form.SetActive(true);
        mainMenu.ShowLoading(false);

        if (res.data != null)
        {
            mainMenu.GoLoginAfterVerify(email);
        }
        else
        {
            string errorMessage = res.error;
            mainMenu.ShowErrorPopup(errorMessage, "Verify");
        }

    }
}
