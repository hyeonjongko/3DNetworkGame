using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UI_Room : MonoBehaviourPunCallbacks
{
    private List<UI_RoomItem> _roomItems;
    private Dictionary<string, RoomInfo> _rooms = new();

    private void Awake()
    {
        _roomItems = GetComponentsInChildren<UI_RoomItem>().ToList();

        HideAllRoomUI();
    }

    private void HideAllRoomUI()
    {
        foreach (UI_RoomItem roomItem in _roomItems)
        {
            roomItem.gameObject.SetActive(false);
        }
    }

    // 로비에 입장 후 방 내용(개수, 이름 등등등)이 바뀌면 자동으로 호출되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList.Count);
        // 모든 UI를 비활성화 하고,
        HideAllRoomUI();

        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                _rooms.Remove(room.Name); // Delete
            }
            else
            {
                _rooms[room.Name] = room; // Add or Update
            }
        }

        int roomCount = _rooms.Count;
        List<RoomInfo> rooms = _rooms.Values.ToList();
        for (int i = 0; i < roomCount; i++)
        {
            // 방 개수만큼만 UI를 활성화한다.
            _roomItems[i].Init(rooms[i]);
            _roomItems[i].gameObject.SetActive(true);
        }
    }
}