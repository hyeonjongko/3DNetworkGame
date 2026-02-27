using Photon.Pun;
using UnityEngine;

public class ItemObjectFactory : MonoBehaviour
{
    public static ItemObjectFactory Instance { get; private set; }

    private PhotonView _photonView;

    private void Awake()
    {
        Instance = this;

        _photonView = GetComponent<PhotonView>();
    }

    //우리의 약속 : 방장에게 룸 관련해서 뭔가 요청을 할 때는 메서드명에 Request로 시작하는게 유지보수가 편하다.
    public void RequestMakeScoreItems(Vector3 makePosition)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            //방장이라면 그냥 함수 호춯
            MakeScoreItems(makePosition);
        }
        else
        {
            //아니라면 방장의 함수를 호출
            _photonView.RPC(nameof(MakeScoreItems), RpcTarget.MasterClient, makePosition);
        }
    }

    [PunRPC]
    private void MakeScoreItems(Vector3 makePosition)
    {
        int randomCount = UnityEngine.Random.Range(3, 5);

        for (int i = 0; i < randomCount; i++)
        {
            PhotonNetwork.InstantiateRoomObject("ScoreItem", makePosition, Quaternion.identity); // RoomObject는 방장만 가지고 있다.
        }


    }

    public void RequestDelete(int viewID)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Delete(viewID);
        }
        else
        {
            _photonView.RPC(nameof(Delete), RpcTarget.MasterClient, viewID);
        }
    }

    [PunRPC]
    private void Delete(int viewId)
    {
        GameObject objectToDelete = PhotonView.Find(viewId).gameObject;
        if (objectToDelete == null) return;

        PhotonNetwork.Destroy(objectToDelete);
    }
}
