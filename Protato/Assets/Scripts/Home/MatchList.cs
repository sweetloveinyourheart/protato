using System.Collections.Generic;
using UnityEngine;

public class MatchList : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] MatchHistory matchHistoryPrefab;
    [SerializeField] float offset = 50f;

    private float contentHeight;
    private string userId;
    private List<MatchHistory> items;

    // Start is called before the first frame update
    void Start()
    {
        userId = ApiContext.Instance.user._id;
        contentHeight = content.rect.height;
        items = new List<MatchHistory>();
    }

    public void ShowHistory(List<MatchListData> list)
    {
        list.ForEach((data) =>
        {
            MatchHistory newMatchHistory = Instantiate(matchHistoryPrefab, content);
            items.Add(newMatchHistory);

            // Write match result
            MatchListResult userRs = data.results.Find(rs => rs.player == userId);
            newMatchHistory.ShowData(data._id, userRs.victory, userRs.score);

            content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);
            contentHeight += offset;
        });
    }

    public void ClearItems()
    {
        if(items != null)
        {
            foreach (MatchHistory item in items)
            {
                Destroy(item.gameObject);
            }

            items.Clear();
        }
    }
}
