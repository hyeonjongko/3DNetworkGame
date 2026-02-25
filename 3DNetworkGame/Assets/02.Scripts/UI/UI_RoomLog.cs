using Photon.Realtime;
using TMPro;
using UnityEngine;

public class UI_RoomLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _logText;

    private void Start()
    {
        _logText.text = "방에 입장했습니다.";

        PhotonRoomManager.Instance.OnPlayerEnter += OnPlayerEnterLog;
        PhotonRoomManager.Instance.OnPlayerLeft += OnPlayerExitLog;
        PhotonRoomManager.Instance.OnPlayerDeathed += PlayerDeathLog;

    }

    private void OnPlayerEnterLog(Player newPlayer)
    {
        _logText.text += "\n" + $"{newPlayer.NickName}님이 입장하였습니다.";
    }
    private void OnPlayerExitLog(Player newPlayer)
    {
        _logText.text += "\n" + $"{newPlayer.NickName}님이 톼장하였습니다.";
    }

    private void PlayerDeathLog(string attackerNickname, string victimNickname)
    {
        _logText.text += "\n" + $"{attackerNickname}님이 {victimNickname}님을 처치하였습니다.";
    }
}