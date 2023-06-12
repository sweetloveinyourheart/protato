using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchResult : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image medal;
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowContent(int place, ResultData result)
    {
        if(place == 0)
        {
            medal.sprite = sprites[1];
        } else
        {
            medal.sprite = sprites[0];
        }

        username.text = result.player.username;
        score.text = result.score.ToString();
    }
}
