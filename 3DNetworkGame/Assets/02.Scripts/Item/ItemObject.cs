using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ItemObject : MonoBehaviourPun // 포톤뷰를 자동으로 GetComponent해서 읽어온다.
{
    private void OnTriggerEnter(Collider other)
    {
        Player player = GetComponent<Player>();

        if (other.CompareTag("Player"))
        {
            Debug.Log("아이템 충돌!");

            other.GetComponent<PlayerController>().Score += 100;


            ItemObjectFactory.Instance.RequestDelete(photonView.ViewID);
        }
    }
}
