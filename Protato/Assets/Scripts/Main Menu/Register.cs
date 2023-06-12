using UnityEngine;
using SimpleJSON;

public class Register : MonoBehaviour
{
    [SerializeField] GameObject form;

    string email = "";
    string password = "";

    // Start is called before the first frame update
    void Start()
    {

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
        form.SetActive(false);
        MainMenu.Instance.ShowLoading(true);

        // Create a JSON object 
        JSONObject data = new JSONObject();
        data["email"] = email;
        data["password"] = password;

        StartCoroutine(ApiContext.Instance.Post("/auth/register", data, HandleResponse));
    }

    void HandleResponse(ApiResponse res)
    {
        MainMenu.Instance.ShowLoading(false);
        form.SetActive(true);

        if (res.data != null)
        {
            MainMenu.Instance.ShowVerifyForm(email);
        }
        else
        {
            string errorMessage = res.error;
            MainMenu.Instance.ShowErrorPopup(errorMessage, "Register");
        }
    }
}
