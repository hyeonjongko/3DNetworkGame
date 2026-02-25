using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RoomInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI    _roomNameTextUI;
    [SerializeField] private TextMeshProUGUI    _playerCountTextUI;
    [SerializeField] private Button             _roomExitButton;
    void Start()
    {
        _roomExitButton.onClick.AddListener(ExitRoom);

        PhotonRoomManager.instance.OnDataChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        Room room = PhotonRoomManager.instance.Room;
        if (room == null) return;

        _roomNameTextUI.text = room.Name;
        _playerCountTextUI.text = $"{room.PlayerCount}/{room.MaxPlayers}";
    }

    private void ExitRoom()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
