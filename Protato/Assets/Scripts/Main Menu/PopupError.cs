using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupError : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI meshPro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage(string txt)
    {
        meshPro.text = txt;
    }
}
