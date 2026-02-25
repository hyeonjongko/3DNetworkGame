using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    public static PhotonRoomManager Instance {  get; private set; }

    private Room _room;
    public Room Room => _room;

    public event Action OnDataChanged;          //룸 정보가 바뀌었을 때

    public event Action<Player> OnPlayerEnter;  //플레이어가 룸에 참여했을 때

    public event Action<Player> OnPlayerLeft;   //플레이어가 룸에서 퇴장했을 때


    public event Action<string, string> OnPlayerDeathed;   //플레이어가 룸에서 퇴장했을 때

    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    // 방 입장에 성공하면 자동으로 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        _room = PhotonNetwork.CurrentRoom;

        OnDataChanged?.Invoke();

        //Debug.Log("룸 입장 완료!");

        //Debug.Log($"룸: {PhotonNetwork.CurrentRoom.Name}");
        //Debug.Log($"플레이어 인원: {PhotonNetwork.CurrentRoom.PlayerCount}");

        //// 룸에 입장한 플레이어 정보
        //Dictionary<int, Player> roomPlayers = PhotonNetwork.CurrentRoom.Players;
        //foreach (KeyValuePair<int, Player> player in roomPlayers)
        //{
        //    Debug.Log($"{player.Value.NickName} : {player.Value.ActorNumber}");
        //}

        int _spawnIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);

        // 리소스 폴더에서 "Player" 이름을 가진 프리팹을 생성(인스턴스화)하고, 서버에 등록도한다.
        //   ㄴ 리소스 폴더는 나쁜것이다. 그러기 때문에 다른 방법을 찾아보거라..
        PhotonNetwork.Instantiate("Player", _spawnPoints[_spawnIndex].position, Quaternion.identity);
    }

    //새로운 플레이어가 방에 입장하면 자동으로 호출되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        OnDataChanged?.Invoke();
        OnPlayerEnter?.Invoke(newPlayer);
    }

    //새로운 플레이어가 방에서 퇴장하면 자동으로 호출되는 함수
    public override void OnPlayerLeftRoom(Player Player)
    {
        OnDataChanged?.Invoke();
        OnPlayerLeft?.Invoke(Player);
    }

    public void OnPlayerDeath(int attackerActorNumber)
    {
        string attackerNickName = _room.Players[attackerActorNumber].NickName;
        string victimNickName = PhotonNetwork.LocalPlayer.NickName;

        OnPlayerDeathed?.Invoke(attackerNickName, victimNickName);
    }
}
