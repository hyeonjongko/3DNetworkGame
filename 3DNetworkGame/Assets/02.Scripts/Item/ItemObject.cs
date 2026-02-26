using Photon.Pun;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Score += 100;

            PhotonNetwork.Destroy(gameObject);
        }
    }
}
