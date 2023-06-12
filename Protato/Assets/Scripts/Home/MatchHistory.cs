using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchHistory : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Image image;

    string matchId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowData(string id, bool victory, int score)
    {
        matchId = id;

        string str = "";
        if (victory == true)
        {
            str += "WIN - " + score.ToString() + " Points";
            content.text = str;
            image.sprite = sprites[1];
        }
        else
        {
            str += "LOST - " + score.ToString() + " Points";
            content.text = str;
            image.sprite = sprites[0];
        }
    }

    public void ShowDetail()
    {
        HistoryManager.Instance.ShowMatchDetail(matchId);
    }
}
