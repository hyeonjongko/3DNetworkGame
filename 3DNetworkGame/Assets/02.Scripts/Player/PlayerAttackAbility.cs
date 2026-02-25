using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackAbility : PlayerAbility
{
    private Animator _animator;

    [SerializeField] private EAnimationSequenceType _animationSequenceType;

    private int _prevAnimationNumber = 0;
    private float _attackTimer = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (!_owner.PhotonView.IsMine) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _attackTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && _attackTimer >= _owner.Stat.AttackSpeed && _owner.Stat.Stamina > _owner.Stat.AttackStamina)
        {
            _attackTimer = 0f;

            int animationNumber = 0;
            switch (_animationSequenceType)
            {
                case EAnimationSequenceType.Sequence:
                {
                    animationNumber = 1 + (_prevAnimationNumber++) % 3;
                    break;
                }
                
                case EAnimationSequenceType.Random:
                {
                    animationNumber = Random.Range(1, 4);
                    break;
                }
            }
            
            _owner.Stat.Stamina -= _owner.Stat.AttackStamina;

            // 1. 일반 메서드 호출 방식
            PlayerAttackAnimation(animationNumber);

            // 2. RPC 메서드 호출 방식
            // 다른 컴퓨터에 있는 내 플레이어 오브젝트의 PlayAttackAnimation 메서드를 실행한다.
            _owner.PhotonView.RPC(nameof(PlayerAttackAnimation), RpcTarget.All, animationNumber);
        }
    }

    // 트랜스폼(위치,회전,스케일), 애니메이션(float파라미터)와 같이 상시로 동기화가 필요한 데이터는 : IPun옵저블(OnPhoton시리얼라이즈View)
    // 애니메이션 트리거처럼 간헐적으로 특정한 이벤트가 발생했을때만 변화하는 데이터 동기화는 데이터 동기화가 아닌 이벤트 동기화 : RPC
    // RPC : Remote Procedure Call (원격 함수 호출)
    //   ㄴ 물리적으로 떨어져 있는 다른 디바이스의 내 포톤뷰의 함수를 호출하는 기능

    // RPC로 호출할 함수는 반드시 [PunRPC] 어트리뷰트를 함수 앞에 명시해줘야한다.
    [PunRPC]
    private void PlayerAttackAnimation(int animationNumber)
    {
        _animator.SetTrigger($"Attack{animationNumber}");
    }
}


public enum EAnimationSequenceType
{
    Sequence,
    Random,
}
