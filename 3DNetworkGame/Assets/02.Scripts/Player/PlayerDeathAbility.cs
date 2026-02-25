using UnityEngine;

public class PlayerDeathAbility : PlayerAbility
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_owner.PhotonView.IsMine) return;

        //플레이어의 체력이 0이하로 떨어진다
        if(_owner.Stat.Health < 0 )
        {
        //죽는 애니메이션 출력
        
        }

    }
}
