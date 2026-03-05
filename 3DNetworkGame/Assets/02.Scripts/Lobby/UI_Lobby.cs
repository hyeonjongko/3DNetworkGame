using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    public GameObject MaleCharacter;
    public GameObject FemaleCharacter;

    public TMP_InputField NicknameInputField;
    public TMP_InputField RoomnameInputField;
    public Button CreateRoomButton;

    private ECharacterType _characterType;

    private void Start()
    {
        CreateRoomButton.onClick.AddListener(MakeRoom);
    }

    private void MakeRoom()
    {
        string nickname = NicknameInputField.text;
        string roomname = RoomnameInputField.text;

        if(string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(roomname))
        {
            return;
        }

        PhotonNetwork.NickName = nickname;

        // 랜덤 룸 입장에 실패하면.. 룸이 하나도 없는 것이니... 룸을 만들자!

        // 룸 옵션 정의
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;  // 룸 최대 접속자 수
        roomOptions.IsVisible = true; // 로비에서 룸을 보여줄 것인지
        roomOptions.IsOpen = true;    // 룸의 오픈 여부

        // 룸 만들기 
        PhotonNetwork.CreateRoom(roomname, roomOptions);
    }

    public void OnClickMale() => OnClickCharacterButton(ECharacterType.Male);
    public void OnClickFemale() => OnClickCharacterButton(ECharacterType.Female);

    private void OnClickCharacterButton(ECharacterType characterType)
    {
        _characterType = characterType;

        MaleCharacter.SetActive(_characterType == ECharacterType.Male);
        FemaleCharacter.SetActive(_characterType == ECharacterType.Female);
    }


}
