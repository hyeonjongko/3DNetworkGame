using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayer()
    {
        int spawnIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        PhotonNetwork.Instantiate("Player", _spawnPoints[spawnIndex].position, Quaternion.identity);
    }

    public void SpawnPlayerDelayed(float delay)
    {
        Invoke(nameof(SpawnPlayer), delay);
    }
}
