using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    public static PhotonRoomManager instance {  get; private set; }

    private Room _room;
    public Room Room => _room;

    public event Action OnDataChanged;

    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    // 방 입장에 성공하면 자동으로 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        _room = PhotonNetwork.CurrentRoom;

        OnDataChanged?.Invoke();

        Debug.Log("룸 입장 완료!");

        Debug.Log($"룸: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"플레이어 인원: {PhotonNetwork.CurrentRoom.PlayerCount}");

        // 룸에 입장한 플레이어 정보
        Dictionary<int, Player> roomPlayers = PhotonNetwork.CurrentRoom.Players;
        foreach (KeyValuePair<int, Player> player in roomPlayers)
        {
            Debug.Log($"{player.Value.NickName} : {player.Value.ActorNumber}");
        }

        int _spawnIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);

        // 리소스 폴더에서 "Player" 이름을 가진 프리팹을 생성(인스턴스화)하고, 서버에 등록도한다.
        //   ㄴ 리소스 폴더는 나쁜것이다. 그러기 때문에 다른 방법을 찾아보거라..
        PhotonNetwork.Instantiate("Player", _spawnPoints[_spawnIndex].position, Quaternion.identity);
    }
}
