using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ItemObject : MonoBehaviourPun // 포톤뷰를 자동으로 GetComponent해서 읽어온다.
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("아이템 충돌!");

            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player.PhotonView.IsMine)
            {
                PhotonScoreManager.Instance.AddScore(100);
            }

            if (PhotonNetwork.IsMasterClient)
            {
                ItemObjectFactory.Instance.RequestDelete(photonView.ViewID);
            }
        }
    }
}
