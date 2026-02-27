using Photon.Pun;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTextUI;

    private void Start()
    {
        PhotonScoreManager.Instance.OnScoreChanged += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        _scoreTextUI.text = $"Score : {PhotonNetwork.LocalPlayer.Score}";
    }
}