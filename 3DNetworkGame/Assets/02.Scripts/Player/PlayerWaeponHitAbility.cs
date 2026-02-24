using Photon.Pun;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerWaeponHitAbility : PlayerAbility
{
    private void OnTriggerEnter(Collider other)
    {
        if(!_owner.PhotonView.IsMine) return;

        if (other.transform == _owner.transform) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            //damageable.TakeDamage(_owner.Stat.Damage);

            //상대방 컴퓨터에 있는 상대방을 공격하여 내 컴퓨터에 그 결과를 적용시키는 것
            //상대방의 TakeDamage를 RPC로 호출한다.
            PlayerController Otherplayer = other.GetComponent<PlayerController>();
            Otherplayer.PhotonView.RPC(nameof(damageable.TakeDamage), RpcTarget.All, _owner.Stat.Damage);

            _owner.GetAbility<PlayerWeaponColliderAbility>().DeactiveCollider();
        }
    }
}