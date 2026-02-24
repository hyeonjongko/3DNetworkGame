using UnityEngine;

public class PlayerWeaponColliderAbility : PlayerAbility
{
    [SerializeField] private Collider _collider;

    void Start()
    {
        DeactiveCollider();
    }

    public void ActiveCollider()
    {
        _collider.enabled = true;
    }
    public void DeactiveCollider()
    {
        _collider.enabled = false;
    }

}
