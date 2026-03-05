using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RoomItem : MonoBehaviour
{
    public TextMeshProUGUI RoomNameTextUI;
    public TextMeshProUGUI MasterNickNameTextUI;
    public TextMeshProUGUI PlayerCountTextUI;

    public Button RoomEnterButton;

    private RoomInfo _roomInfo;

    private void Start()
    {
        RoomEnterButton.onClick.AddListener(EnterRoom);
    }

    public void Init(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;

        RoomNameTextUI.text = roomInfo.Name;
        MasterNickNameTextUI.text = "방장 이름";
        PlayerCountTextUI.text = $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}";
    }

    private void EnterRoom()
    {
        if (_roomInfo == null) return;

        PhotonNetwork.JoinRoom(_roomInfo.Name);
    }


}