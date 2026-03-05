using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        SpawnManager.Instance.SpawnPlayer();
    }


}
