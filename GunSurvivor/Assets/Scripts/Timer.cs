using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int _minutes = 0;
    private int _seconds = 0;

    private TextMeshProUGUI _timerText;
    private EnemySpawnManager _enemySpawnManager;

    private void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
        _enemySpawnManager = FindObjectOfType<EnemySpawnManager>().GetComponent<EnemySpawnManager>();

        StartCoroutine(TimerSystem());
    }

    private IEnumerator TimerSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _seconds += 1;

            if (_seconds == 60)
            {
                _seconds = 0;
                _minutes++;

                SpawnEnemies();
            }

            if (_seconds == 30)
            {
                SpawnEnemies();
            }

            if (_seconds < 10) _timerText.text = $"{_minutes}:0{_seconds}";
            else _timerText.text = $"{_minutes}:{_seconds}";
        }
    }

    private void SpawnEnemies()
    {
        _enemySpawnManager.SpawnEnemies(_enemySpawnManager.EnemyCountToSpawn);
        _enemySpawnManager.SpawnedWaves++;

        if (_enemySpawnManager.SpawnedWaves == 2) _enemySpawnManager.Difficult++;

        if (_enemySpawnManager.SpawnedWaves % 20 == 0)
        {
            _enemySpawnManager.SpawnBoss();
            _enemySpawnManager.Difficult++;
        }
    }
}
