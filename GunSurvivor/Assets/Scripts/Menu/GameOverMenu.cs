using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private StatisticCounter _statisticCounter;

    [SerializeField] private TextMeshProUGUI _killedEnemiesText;
    [SerializeField] private TextMeshProUGUI _outgoingDamageText;
    [SerializeField] private TextMeshProUGUI _incomingDamageText;
    [SerializeField] private TextMeshProUGUI _collectedCoinsText;
    [SerializeField] private TextMeshProUGUI _collectedExpText;

    private void Start()
    {
        _killedEnemiesText.text = $"Killed enemies - {_statisticCounter.KilledEnemies}";
        _outgoingDamageText.text = $"Outgoing damage - {_statisticCounter.OutGoingDamage}";
        _incomingDamageText.text = $"Incoming damage - {_statisticCounter.IncomingDamage}";
        _collectedCoinsText.text = $"Collected coins - {_statisticCounter.CollectedCoins}";
        _collectedExpText.text = $"Collected exp - {_statisticCounter.CollectedExperiense}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
