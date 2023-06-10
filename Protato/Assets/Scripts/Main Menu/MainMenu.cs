using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Login login;
    [SerializeField] Register register;
    [SerializeField] ValidateAccount validateAccount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRegisterClick()
    {
        login.gameObject.SetActive(false);
        validateAccount.gameObject.SetActive(false);
        register.gameObject.SetActive(true);
    }

    public void OnLoginClick()
    {
        validateAccount.gameObject.SetActive(false);
        register.gameObject.SetActive(false);
        login.gameObject.SetActive(true);
    }
}
