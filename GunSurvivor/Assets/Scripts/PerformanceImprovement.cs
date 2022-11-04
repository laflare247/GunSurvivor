using TMPro;
using UnityEngine;

public class PerformanceImprovement : MonoBehaviour
{
    private CharacterController _player;
    private UserInterface _userInterface;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _availableUpdatePointsText;
    [SerializeField] private TextMeshProUGUI _maxHealthLevelText;
    [SerializeField] private TextMeshProUGUI _speedLevelText;
    [SerializeField] private TextMeshProUGUI _bulletSpeedLevelText;
    [SerializeField] private TextMeshProUGUI _shootCooldownLevelText;
    [SerializeField] private TextMeshProUGUI _damageLevelText;
    [SerializeField] private TextMeshProUGUI _collectDistanceLevelText;

    private void Start()
    {
        _player = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();

        _userInterface = FindObjectOfType<UserInterface>().GetComponent<UserInterface>();
    }

    private float CalculatePercents(float percentsFrom, float percents)
    {
        return (percentsFrom / 100) * percents;
    }

    public void UpdateAviableUpdatePointsText()
    {
        _availableUpdatePointsText.text = $"Aviable Points {_player.AviableUpdatePoints}";
    }

    public void ImproveMaxHealth()
    {
        if (_player.AviableUpdatePoints > 0 && _player.MaxHealthLevel < _player.MaxLevel)
        {
            float improveValue = CalculatePercents(_player.MaxHealth, 10);

            _player.MaxHealth += improveValue;
            _player.PlayerHealth += improveValue;
            _player.AviableUpdatePoints--;
            _player.MaxHealthLevel++;

            _userInterface.HealthSliderMaxValue = _player.MaxHealth;

            UpdateAviableUpdatePointsText();
            UpdateMaxHealthLevelText();
            _userInterface.UpdateHealthBar();

        }
    }

    private void UpdateMaxHealthLevelText()
    {
        _maxHealthLevelText.text = $"Max HP {_player.MaxHealthLevel}";
    }

    public void ImproveSpeed()
    {
        if (_player.AviableUpdatePoints > 0 && _player.SpeedLevel < _player.MaxLevel)
        {
            if (_player.Speed < _player.MaxSpeed) _player.Speed += CalculatePercents(_player.Speed, 5);
            else _player.Speed += CalculatePercents(_player.Speed, 1);
            _player.AviableUpdatePoints--;
            _player.SpeedLevel++;

            UpdateAviableUpdatePointsText();
            UpdateSpeedLevelText();
        }
    }

    private void UpdateSpeedLevelText()
    {
        _speedLevelText.text = $"Speed {_player.SpeedLevel}";
    }

    public void ImproveBulletSpeed()
    {
        if (_player.AviableUpdatePoints > 0 && _player.BulletSpeedLevel < _player.MaxLevel)
        {
            if (_player.BulletSpeed < _player.MaxBulletSpeed) _player.BulletSpeed += CalculatePercents(_player.BulletSpeed, 5);
            else _player.BulletSpeed += CalculatePercents(_player.BulletSpeed, 1);
            _player.AviableUpdatePoints--;
            _player.BulletSpeedLevel++;

            UpdateAviableUpdatePointsText();
            UpdateBulletSpeedLevelText();
        }
    }

    private void UpdateBulletSpeedLevelText()
    {
        _bulletSpeedLevelText.text = $"Bullet Speed {_player.BulletSpeedLevel}";
    }

    public void ImproveShootCooldown()
    {
        if (_player.AviableUpdatePoints > 0 && _player.ShootCooldownLevel < _player.MaxLevel)
        {
            _player.ShootCooldown -= CalculatePercents(_player.ShootCooldown, 5);
            _player.AviableUpdatePoints--;
            _player.ShootCooldownLevel++;

            UpdateAviableUpdatePointsText();
            UpdateShootCooldownLevelText();
        }
    }

    private void UpdateShootCooldownLevelText()
    {
        _shootCooldownLevelText.text = $"Shoot Cooldown {_player.ShootCooldownLevel}";
    }

    public void ImproveDamage()
    {
        if (_player.AviableUpdatePoints > 0 && _player.DamageLevel < _player.MaxLevel)
        {
            _player.PlayerDamage += CalculatePercents(_player.PlayerDamage, 20);
            _player.AviableUpdatePoints--;
            _player.DamageLevel++;

            UpdateAviableUpdatePointsText();
            UpdateDamageLevelText();
        }
    }

    private void UpdateDamageLevelText()
    {
        _damageLevelText.text = $"Damage {_player.DamageLevel}";
    }

    public void ImproveCollectDistance()
    {
        if (_player.AviableUpdatePoints > 0 && _player.CollectDistanceLevel < _player.MaxLevel)
        {
            _player.CollectDistance += CalculatePercents(_player.CollectDistance, 20);
            _player.AviableUpdatePoints--;
            _player.CollectDistanceLevel++;

            UpdateAviableUpdatePointsText();
            UpdateCollectDistanceLevelText();
        }
    }

    private void UpdateCollectDistanceLevelText()
    {
        _collectDistanceLevelText.text = $"Collect Distance {_player.CollectDistanceLevel}";
    }
}
