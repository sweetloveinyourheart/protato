using SimpleJSON;
using UnityEngine;

public class ValidateAccount : MonoBehaviour
{
    [SerializeField] GameObject form;

    string validationCode;
    string email;

    // Start is called before the first frame update
    void Start()
    {
        
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
        form.SetActive(false);
        MainMenu.Instance.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["code"] = validationCode;

        StartCoroutine(ApiContext.Instance.Post("/auth/verify-account", data, HandleResponse));
    }

    void HandleResponse(ApiResponse res)
    {
        form.SetActive(true);
        MainMenu.Instance.ShowLoading(false);

        if (res.data != null)
        {
            MainMenu.Instance.GoLoginAfterVerify(email);
        }
        else
        {
            string errorMessage = res.error;
            MainMenu.Instance.ShowErrorPopup(errorMessage, "Verify");
        }
    }
}
