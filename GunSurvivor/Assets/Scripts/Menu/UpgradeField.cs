using TMPro;
using UnityEngine;

public class UpgradeField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHealthLevelText;
    [SerializeField] private TextMeshProUGUI _maxHealthPriceText;

    [SerializeField] private TextMeshProUGUI _damageLevelText;
    [SerializeField] private TextMeshProUGUI _damagePriceText;

    [SerializeField] private TextMeshProUGUI _shootCooldownLevelText;
    [SerializeField] private TextMeshProUGUI _shootCooldownPriceText;

    [SerializeField] private TextMeshProUGUI _collectDistanceLevelText;
    [SerializeField] private TextMeshProUGUI _collectDistancePriceText;

    private int _maxHealthPrice;
    private int _damagePrice;
    private int _shootCooldownPrice;
    private int _collectDistancePrice;

    private void Awake()
    {
        Update_MaxHealth();
        Update_Damage();
        Update_ShootCooldown();
        Update_CollectDistance();
    }

    public void CloseUpgradeField()
    {
        gameObject.SetActive(false);
    }

    public void MaxHealthUpgrade()
    {
        if (PlayerPrefs.GetInt("Coins", 0) >= _maxHealthPrice)
        {
            PlayerPrefs.SetInt("MaxHealthLevel", PlayerPrefs.GetInt("MaxHealthLevel", 1) + 1);

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - _maxHealthPrice);
            Update_Coins();

            PlayerPrefs.Save();

            Update_MaxHealth();
        }
    }

    private void Update_MaxHealth()
    {
        _maxHealthPrice = PlayerPrefs.GetInt("MaxHealthLevel", 1) * 100;
        _maxHealthLevelText.text = $"Max Health {PlayerPrefs.GetInt("MaxHealthLevel", 1)}";
        _maxHealthPriceText.text = _maxHealthPrice.ToString();
    }

    public void DamageUpgrade()
    {
        if (PlayerPrefs.GetInt("Coins", 0) >= _damagePrice)
        {
            PlayerPrefs.SetInt("DamageLevel", PlayerPrefs.GetInt("DamageLevel", 1) + 1);

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - _damagePrice);
            Update_Coins();

            PlayerPrefs.Save();

            Update_Damage();
        }
    }

    private void Update_Damage()
    {
        _damagePrice = PlayerPrefs.GetInt("DamageLevel", 1) * 100;
        _damageLevelText.text = $"Damage {PlayerPrefs.GetInt("DamageLevel", 1)}";
        _damagePriceText.text = _damagePrice.ToString();
    }

    public void ShootCooldownUpgrade()
    {
        if (PlayerPrefs.GetInt("Coins", 0) >= _shootCooldownPrice)
        {
            PlayerPrefs.SetInt("ShootCooldownLevel", PlayerPrefs.GetInt("ShootCooldownLevel", 1) + 1);

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - _shootCooldownPrice);
            Update_Coins();

            PlayerPrefs.Save();

            Update_ShootCooldown();
        }
    }

    private void Update_ShootCooldown()
    {
        _shootCooldownPrice = PlayerPrefs.GetInt("ShootCooldownLevel", 1) * 100;
        _shootCooldownLevelText.text = $"Shoot Cooldown {PlayerPrefs.GetInt("ShootCooldownLevel", 1)}";
        _shootCooldownPriceText.text = _shootCooldownPrice.ToString();
    }

    public void CollectDistanceUpgrade()
    {
        if (PlayerPrefs.GetInt("Coins", 0) >= _collectDistancePrice)
        {
            PlayerPrefs.SetInt("CollectDistanceLevel", PlayerPrefs.GetInt("CollectDistanceLevel", 1) + 1);

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - _collectDistancePrice);
            Update_Coins();

            PlayerPrefs.Save();

            Update_CollectDistance();
        }
    }

    private void Update_CollectDistance()
    {
        _collectDistancePrice = PlayerPrefs.GetInt("CollectDistanceLevel", 1) * 100;
        _collectDistanceLevelText.text = $"Collect Distance {PlayerPrefs.GetInt("CollectDistanceLevel", 1)}";
        _collectDistancePriceText.text = _collectDistancePrice.ToString();
    }

    private void Update_Coins()
    {
        FindObjectOfType<MainMenu>().Update_Coins();
    }
}