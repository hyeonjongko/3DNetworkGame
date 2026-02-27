using Photon.Pun;
using UnityEngine;

public class PlayerDeathAbility : PlayerAbility
{
    [SerializeField] private float _time = 0;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_owner.PhotonView.IsMine) return;

        //플레이어의 체력이 0이하로 떨어진다
        if(_owner.Stat.Health <= 0 && !_owner.Stat.Dead)
        {
            _owner.Stat.Dead = true;
            //죽는 애니메이션 출력 - 모든 클라이언트에 RPC로 전달
            _owner.PhotonView.RPC("PlayDeathAnimation", RpcTarget.All);
        }

        if (_owner.Stat.Dead)
        {
            _time += Time.deltaTime;

            if (_time > 2)
            {
                // 플레이어를 파괴하기 전에 SpawnManager에 리스폰 요청 (3초 후 = 총 5초)
                SpawnManager.Instance.SpawnPlayerDelayed(3f);
                // PhotonNetwork.Destroy로 모든 클라이언트에서 오브젝트 파괴
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    private void PlayDeathAnimation()
    {
        _animator.SetTrigger("Dead");
    }
}
