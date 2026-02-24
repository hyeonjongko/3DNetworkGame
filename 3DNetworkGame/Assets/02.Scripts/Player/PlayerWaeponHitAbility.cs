using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerWaeponHitAbility : PlayerAbility
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _owner.transform) return;

        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(_owner.Stat.Damage);
        }
    }
}