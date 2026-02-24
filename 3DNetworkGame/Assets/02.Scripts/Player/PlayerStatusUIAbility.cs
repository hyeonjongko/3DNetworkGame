using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUIAbility : PlayerAbility
{
    [SerializeField] private Image _healthGuage;
    [SerializeField] private Image _staminaGuage;

    private void Update()
    {
        _healthGuage.fillAmount = _owner.Stat.Health / _owner.Stat.MaxHealth;
        _staminaGuage.fillAmount = _owner.Stat.Stamina / _owner.Stat.MaxStamina;
    }

}
