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

            //1. �Ϲ� �޼��� ȣ�� ���
            PlayerAttackAnimation(animationNumber);

            //2. RPC �޼��� ȣ�� ���
            //�ٸ� ��ǻ�Ϳ� �ִ� �� �÷��̾� ������Ʈ�� PlayerAttackAnimation �޼��带 ȣ���Ѵ�.
            _owner.PhotonView.RPC(nameof(PlayerAttackAnimation), RpcTarget.All, animationNumber);
        }
    }

    //Ʈ������(��ġ, ȸ��, ������), �ִϸ��̼�(float�Ķ����)�� ���� ��÷� ����ȭ�� �ʿ��� �����ʹ� : IPunObserable(OnPhotonSerializeView)
    //�ִϸ��̼� Ʈ����ó�� ���������� Ư���� �̺�Ʈ�� �߻����� ���� ��ȭ�ϴ� ������ ����ȭ�� ������ ����ȭ�� �ƴ� �̺�Ʈ ����ȭ : RPC
    // RPC : Remote Procedure Call (���� �Լ� ȣ��)
    //  �� ���������� ������ �ִ� �ٸ� ����̽��� �Լ��� ȣ���ϴ� ���

    //RPC�� ȣ���� �Լ��� �ݵ�� [PunRPC] ��Ʈ����Ʈ�� �Լ� �տ� �������־�� �Ѵ�.
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
