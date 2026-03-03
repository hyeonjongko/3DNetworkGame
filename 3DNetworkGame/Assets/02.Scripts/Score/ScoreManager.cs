using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance { get; private set; }

    private int _score;

    private Dictionary<int, ScoreData> _scores = new();
    public ReadOnlyDictionary<int, ScoreData> Scores => new ReadOnlyDictionary<int, ScoreData>(_scores);
    //public Dictionary<int ,ScoreData> Scores => _scores;
    //외부에서 수정 못 하게 ReadOnlyDictionary로 반환

    public static event Action OnDataChanged;

    private void Awake()
    {
        Instance = this;
    }

    public override void OnJoinedRoom()
    {
        //방에 들어가면 내 점수가 0점이다라고 초기화
        Refresh();
    }

    public void AddScore(int score)
    {
        _score += score;

        Refresh();
    }

    public void ReduceScore()
    {
        _score /= 2;
        Refresh();
    }

    private void Refresh()
    {
        //헤시테이블은 딕션너리와 같은 키-값 형태로 저장하는데
        //                             키-값에 있어서 자료형이 object이다.
        Hashtable hashtable = new Hashtable();
        hashtable.Add("score", _score);

        //프로퍼티 설정
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!changedProps.ContainsKey("score")) return;

        ScoreData scoreData = new ScoreData()
        {
            Nickname = targetPlayer.NickName,
            Score = (int)changedProps["score"]
        };

        _scores[targetPlayer.ActorNumber] = scoreData;

        OnDataChanged?.Invoke();


        //int score = (int)changedProps["score"];
        //Debug.Log($"Player{targetPlayer.NickName}'의 점수는 : {score}");
    }

    // [데이터 공유]
    // 1. OnSerializeView (+TransformView, AnimatiorView, ...)
    //    ㄴ C# 기본 타입, Vector, 
    //    ㄴ PhotonNetwork... Rate... 에 따라

    // 2. RPC -> 매개변수를 활용해서 데이터 동기화 
    //     ㄴ 주로 변화가 빈번하지 않은 데이터를 함수 호출을 이용해서 동기화..

    // 3. 커스텀 프로퍼티(Custom Property)
    //    ㄴ 주로 변화가 빈번하지 않은 데이터들을 해시 테이블로 동기화...
    //    ㄴ (플레이어 준비 상태, 점수, 룸의 모드, 맵 선택 등..)


}
