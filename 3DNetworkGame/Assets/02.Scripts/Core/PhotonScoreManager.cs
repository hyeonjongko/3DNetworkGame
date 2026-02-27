using System;
using Photon.Pun;
using UnityEngine;

public class PhotonScoreManager : MonoBehaviour
{
    public static PhotonScoreManager Instance { get; private set; }

    public event Action OnScoreChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        PhotonNetwork.LocalPlayer.Score += amount;
        OnScoreChanged?.Invoke();
    }
}