using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    private CharacterController _player;

    private StatisticCounter _statisticCounter;

    [SerializeField] private TextMeshProUGUI _playerLevelText;
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [SerializeField] private GameObject _experienceBar;
    private Slider _experienceBarSlider;

    [SerializeField] private GameObject _healthBar;
    private Slider _heatlhBarSlider;


    public float ExperienceSliderMaxValue
    {
        get { return _experienceBarSlider.maxValue; }
        set { if (value > 0) _experienceBarSlider.maxValue = value; }
    }

    public float HealthSliderMaxValue
    {
        get { return _heatlhBarSlider.maxValue; }
        set { if (value > 0) _heatlhBarSlider.maxValue = value; }
    }


    private void Start()
    {
        _player = FindObjectOfType<CharacterController>();
        _statisticCounter = FindObjectOfType<StatisticCounter>();
        _experienceBarSlider = _experienceBar.GetComponent<Slider>();
        _heatlhBarSlider = _healthBar.GetComponent<Slider>();

        _heatlhBarSlider.maxValue = _player.MaxHealth;
        UpdateHealthBar();
        UpdateCoinsText();
    }

    public void UpdateCoinsText()
    {
        _coinsText.text = _statisticCounter.CollectedCoins.ToString();
    }

    public void UpdateExperienceBar()
    {
        _experienceBarSlider.value = _player.CurrentExperience;
    }

    public void UpdateHealthBar()
    {
        _heatlhBarSlider.value = _player.PlayerHealth;
        _playerHealthText.text = ((int)_player.PlayerHealth).ToString();
    }

    public void UpdatePlayerLevel()
    {
        _playerLevelText.text = $"Level: {_player.ExperienceLevel}";
    }

    public void ActivateGameOverMenu()
    {
        transform.Find("GameOverMenu").gameObject.SetActive(true);
    }

    public void ActivatePauseMenu()
    {
        transform.Find("PauseMenu").gameObject.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        transform.Find("PauseMenu").gameObject.SetActive(false);
    }
}