using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Login login;
    [SerializeField] Register register;
    [SerializeField] ValidateAccount validateAccount;
    [SerializeField] PopupError popupError;

    [SerializeField] GameObject loadingObj;
    string currentPopup = "Login";

    public void OnRegisterClick()
    {
        login.gameObject.SetActive(false);
        register.gameObject.SetActive(true);
        currentPopup = "Register";
    }

    public void OnLoginClick()
    {
        register.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
        currentPopup = "Login";
    }

    public void ShowLoading(bool show)
    {
        loadingObj.SetActive(show);
    }

    public void ShowErrorPopup(string err, string forScreen)
    {
        currentPopup = forScreen;

        if (currentPopup == "Login") login.gameObject.SetActive(false);
        if (currentPopup == "Register") register.gameObject.SetActive(false);
        if (currentPopup == "Verify") validateAccount.gameObject.SetActive(false);

        popupError.gameObject.SetActive(true);
        popupError.ShowMessage(err);
    }

    public void CloseErrorPopup()
    {

        if (currentPopup == "Login") login.gameObject.SetActive(true);
        if (currentPopup == "Register") register.gameObject.SetActive(true);
        if (currentPopup == "Verify") validateAccount.gameObject.SetActive(true);

        popupError.gameObject.SetActive(false);
    }

    public void ShowVerifyForm(string email)
    {

        register.gameObject.SetActive(false);
        validateAccount.gameObject.SetActive(true);
        validateAccount.SetVerifyEmail(email);
        currentPopup = "Verify";
    }

    public void GoLoginAfterVerify(string email)
    {
        validateAccount.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
        login.HandleEmailEdit(email);
        currentPopup = "Login";
    }
}
