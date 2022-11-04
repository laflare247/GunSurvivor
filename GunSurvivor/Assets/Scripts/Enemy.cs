using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController _player;
    private Rigidbody2D _rb;

    private StatisticCounter _statisticCounter;

    [Header("Indicators")]
    [SerializeField] private float _enemyHealth = 100;
    [SerializeField] private GameObject _damageIndicator;

    [Header("Movement")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _enemyOffset = 4f;
    private float _startSpeed;

    [Header("Shooting")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed = 200;
    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _shootOffset = 16f;
    private bool _canShoot = true;

    [Header("Drop")]
    [SerializeField] private GameObject _experienceObject;
    [SerializeField] private GameObject _healObject;
    [SerializeField] private GameObject _coinObject;

    public float EnemyHealth
    {
        get { return _enemyHealth; }
        set { _enemyHealth = value; }
    }

    public float EnemySpeed
    {
        get { return _speed; }
        set { if (value > 0) _speed = value; }
    }

    public float EnemyShootCooldown
    {
        get { return _shootCooldown; }
        set { if (value > 0) _shootCooldown = value; }
    }

    private void Start()
    {
        _player = FindObjectOfType<CharacterController>();
        _rb = GetComponent<Rigidbody2D>();
        _startSpeed = _speed;

        _statisticCounter = FindObjectOfType<StatisticCounter>();
    }

    private void Update()
    {
        if (_player.IsAlive) LookAtPlayer();
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
        Shoot();
    }

    private void LookAtPlayer()
    {
        Vector2 enemyPosToPlayer = _player.transform.position - transform.position;

        float angle = Mathf.Atan2(enemyPosToPlayer.x, enemyPosToPlayer.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    private void MoveToPlayer()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);

        if (distance > _enemyOffset)
        {
            Vector2 directionToMove = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.fixedDeltaTime);

            _rb.MovePosition(directionToMove);
        }
    }

    public virtual void Shoot()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);

        if (_canShoot && distance < _shootOffset)
        {
            Vector3 direction = transform.up;

            GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, transform.rotation);

            Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();

            _bulletRb.AddForce(direction * _bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

            _canShoot = false;
            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }


    public void ApplyDamage(float damage, bool isCrit = false)
    {
        if (damage > 0)
        {
            EnemyHealth -= damage;

            _statisticCounter.OutGoingDamage += (int)damage;

            if (isCrit)
            {
                _damageIndicator.GetComponent<TextMeshPro>().color = _damageIndicator.GetComponent<DamageIndicator>().CriticalDamageColor;
            }
            else
            {
                _damageIndicator.GetComponent<TextMeshPro>().color = _damageIndicator.GetComponent<DamageIndicator>().DefaultColor;
            }

            ShowDamageAmount((int)damage);

            if (EnemyHealth <= 0)
            {
                Die();
            }
        }
    }

    private void ShowDamageAmount(float damage)
    {
        _damageIndicator.GetComponent<TextMeshPro>().text = $"-{damage}";
        Instantiate(_damageIndicator, transform.position, Quaternion.Euler(0, 0, 0));
    }

    private void Die()
    {
        Destroy(gameObject);

        _statisticCounter.KilledEnemies++;

        int randomNumber = Random.Range(0, 20);
    
        if (randomNumber > 10 && randomNumber < 15)
        {
            Instantiate(_coinObject, new Vector3(transform.position.x, transform.position.y, _experienceObject.transform.position.z), _coinObject.transform.rotation);
        }
        else if (randomNumber == 10)
        {
            Instantiate(_healObject, new Vector3(transform.position.x, transform.position.y, _experienceObject.transform.position.z), _healObject.transform.rotation);
        }
        else
        {
            Instantiate(_experienceObject, new Vector3(transform.position.x, transform.position.y, _experienceObject.transform.position.z), _experienceObject.transform.rotation);
        }
    }

    public void Burn()
    {
        float fireDamage = _player.PlayerDamage / 5;
        int burnCount = 3;

        StartCoroutine(ApplyBurnDamage(fireDamage, burnCount));
    }

    private IEnumerator ApplyBurnDamage(float fireDamage, int burnCount)
    {
        for (int i = 0; i < burnCount; i++)
        {
            yield return new WaitForSeconds(1);
            ApplyDamage(fireDamage);
        }
    }

    public void SlowDown(float slownessValue, float slownessTime)
    {
        StartCoroutine(SlowDownCoroutine(slownessValue, slownessTime));
    }

    private IEnumerator SlowDownCoroutine(float slownessValue, float slownessTime)
    {
        if (_speed * slownessValue == _startSpeed * slownessValue)
        {
            _speed *= slownessValue;
        }

        yield return new WaitForSeconds(slownessTime);

        _speed = _startSpeed;
    }
}