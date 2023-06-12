using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDetail : MonoBehaviour
{
    [SerializeField] MatchResult matchResultPrefab;
    [SerializeField] RectTransform content;

    List<MatchResult> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<MatchResult>();
    }

    public void ShowDetail(MatchData detail)
    {
        List<ResultData> temp = detail.results;
        temp.Sort((p1, p2) => p1.score.CompareTo(p2.score));

        int place = 0;
        temp.ForEach((data) =>
        {
            MatchResult matchResult = Instantiate(matchResultPrefab, content);
            items.Add(matchResult);

            matchResult.ShowContent(place, data);
            place++;
        });
    }

    public void ClearItems()
    {
        if(items != null)
        {
            foreach (MatchResult item in items)
            {
                Destroy(item.gameObject);
            }

            items.Clear();
        }
    }
}
