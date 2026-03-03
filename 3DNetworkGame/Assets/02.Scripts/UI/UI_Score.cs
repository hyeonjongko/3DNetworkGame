using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    private List<UI_ScoreItem> _items;
    private void Start()
    {
        _items = GetComponentsInChildren<UI_ScoreItem>().ToList();

        ScoreManager.OnDataChanged += Refresh;
        Refresh();
    }

    private void Refresh()
    {
        var scores = ScoreManager.Instance.Scores;

        List<ScoreData> scoreDatas = scores.Values.ToList();

        // 1. todo : 1등부터 3등까지 정렬
        //           - 정렬은 이미 매니저에서 해서 넘겨야 하나 vs UI에서 해야하나.. (도메인 규칙에 따라 다르다)
        //           - 필수 : Linq 사용
        // 2. todo : 3명 있는지 적절하게 반복문
        for(int i = 0; i < _items.Count; i++)
        {
            ScoreData data = scoreDatas[i];
            _items[i].Set(data.Nickname, data.Score);
        }

    }
}