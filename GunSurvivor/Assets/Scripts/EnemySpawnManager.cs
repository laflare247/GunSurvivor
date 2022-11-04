using UnityEngine;
using System.Linq;

public class EnemySpawnManager : MonoBehaviour
{
    [Tooltip("Enemies in order of strength")]
    [SerializeField] private GameObject[] _enemies_Easy;
    [Tooltip("Enemies in order of strength")]
    [SerializeField] private GameObject[] _enemies_Medium;
    [Tooltip("Enemies in order of strength")]
    [SerializeField] private GameObject[] _enemies_Hard;
    [SerializeField] private GameObject[] _bosses;

    [SerializeField] private Transform[] _spawnPositions;


    private int _enemyCountToSpawn = 5;
    private int _enemyCountToSpawnLimit = 80;
    private int _difficult = -1;
    private int _spawnedWaves = 0;
    private float _cicleSpawnDelay = 5f;

    private float _upgradeValue_health = 0.5f;
    private float _maxUpgradeValue_health = 8f;

    private float _upgradeValue_speed = 0.5f;
    private float _maxUpgradeValue_speed = 8f;

    private float _upgradeValue_shootCooldown = 1f;
    private float _maxUpgradeValue_shootCooldown = 0.5f;

    private float _upgradeValue_bulletSpeed = 1f;
    private float _maxUpgradeValue_bulletSpeed = 5f;


    private int _spawnedBosses = 0;

    public int EnemyCountToSpawn
    {
        get { return _enemyCountToSpawn; }
        set { if (value > 0) _enemyCountToSpawn = value; }
    }

    public int SpawnedWaves
    {
        get { return _spawnedWaves; }
        set { if (value > 0) _spawnedWaves = value; }
    }

    public int SpawnedBosses
    {
        get { return _spawnedBosses; }
        set { if (value > 0) _spawnedBosses = value; }
    }

    public int Difficult
    {
        get { return _difficult; }
        set { if (value >= 0) _difficult = value; }
    }
    

    private void Start()
    {
        SpawnEnemies(_enemyCountToSpawn);

        Invoke(nameof(CicleSpawn), _cicleSpawnDelay);
    }

    private int RandomChoose(int[] array)
    {
        int randomValue = Random.Range(1, 101);

        if (randomValue <= 60)
        {
            return array[0];
        }
        else if (randomValue <= 90)
        {
            return array[1];
        }
        else if (randomValue <= 100)
        {
            return array[2];
        }

        return 0;
    }

    public void SpawnEnemies(int enemyCount, bool cycle = false)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy;

            int spawnPositionIndex = Random.Range(0, _spawnPositions.Length);
            int enemyIndex = RandomChoose(Enumerable.Range(0, _enemies_Easy.Length).ToArray());

            Vector3 positionToSpawn = _spawnPositions[spawnPositionIndex].position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

            if (_difficult == -1) enemy = Instantiate(_enemies_Easy[0], positionToSpawn, transform.rotation);
            else if (_difficult == 0) enemy = Instantiate(_enemies_Easy[enemyIndex], positionToSpawn, transform.rotation);
            else if (_difficult == 1) enemy = Instantiate(_enemies_Medium[enemyIndex], positionToSpawn, transform.rotation);
            else if (_difficult == 2) enemy = Instantiate(_enemies_Hard[enemyIndex], positionToSpawn, transform.rotation);
            else enemy = Instantiate(_enemies_Hard[enemyIndex], positionToSpawn, transform.rotation);

            enemy.GetComponent<Enemy>().EnemyHealth *= _upgradeValue_health;
            enemy.GetComponent<Enemy>().EnemySpeed *= _upgradeValue_speed;
            enemy.GetComponent<Enemy>().EnemyShootCooldown *= _upgradeValue_shootCooldown;
        }
        if (!cycle && SpawnedWaves >= 5)
        {
            UpgradeEnemies();

            if ((int)Mathf.Round(_enemyCountToSpawn * 1.2f) <= _enemyCountToSpawnLimit)
            {
                _enemyCountToSpawn = (int)Mathf.Round(_enemyCountToSpawn * 1.2f);
            }
        }
    }

    private void UpgradeEnemies()
    {
        if (_upgradeValue_health < _maxUpgradeValue_health) _upgradeValue_health += 0.2f;
        if (_upgradeValue_speed < _maxUpgradeValue_speed) _upgradeValue_speed += 0.05f;
        if (_upgradeValue_shootCooldown > _maxUpgradeValue_shootCooldown) _upgradeValue_shootCooldown -= 0.05f;
        if (_upgradeValue_bulletSpeed < _maxUpgradeValue_bulletSpeed) _upgradeValue_bulletSpeed += 0.05f;

    }

    private void CicleSpawn()
    {
        int enemyCountToSpawn = _enemyCountToSpawn / 10;

        if (enemyCountToSpawn == 0) enemyCountToSpawn = 1;

        SpawnEnemies(enemyCountToSpawn, cycle: true);

        Invoke(nameof(CicleSpawn), _cicleSpawnDelay);
    }

    public void SpawnBoss()
    {
        int spawnPositionIndex = Random.Range(0, _spawnPositions.Length);
        Vector3 positionToSpawn = _spawnPositions[spawnPositionIndex].position;

        if (_bosses != null && _spawnedBosses < _bosses.Length)
        {
            Instantiate(_bosses[_spawnedBosses], positionToSpawn, transform.rotation);
            _spawnedBosses++;
        }
    }
}
