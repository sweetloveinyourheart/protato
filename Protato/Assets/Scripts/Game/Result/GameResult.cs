using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Text.RegularExpressions;

public class GameResult : MonoBehaviour
{
    [SerializeField] Image scoreBoard;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(bool victory, int score)
    {
        if (victory)
        {
            scoreBoard.sprite = sprites[1];
        } else
        {
            scoreBoard.sprite = sprites[0];
        }

        textMeshPro.text = score.ToString();
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Home");
    }
}
