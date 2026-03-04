using UnityEngine;
using UnityEngine.UI;

public class BearStatusUIAbility : BearAbility
{
    [SerializeField] private Image _healthGuage;

    private void Update()
    {
        _healthGuage.fillAmount = _owner.Stat.Health / _owner.Stat.MaxHealth;
    }
}
