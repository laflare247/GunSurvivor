using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Camera _camera;

    private PerformanceImprovement _perfomanceImprovement;
    private UserInterface _userInterface;

    private SelectionUpdate _updateSelectionField;

    private Vector2 currentVelocity = Vector2.zero;

    private StatisticCounter _statisticCounter;

    [Header("Indicators")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 100f;
    private bool _isAlive = true;
    private bool _isPaused = false;

    [Header("Movement")]
    [SerializeField] private float _speed = 300f;
    [SerializeField] [Range(0,1)] private float _movementSmooth = 0.1f;

    [Header("Shooting")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 200;
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private float _damage = 20f;
    private bool _canShoot = true;
    private Transform _firePoint2;
    private float _damageCooldown = 0.1f;
    private bool _enableToDamage = true;

    [Header("Experience")]
    [SerializeField] private float _collectDistance = 4f;
    private float _currentExperience = 0;
    private int _experienceLevel = 1;
    private float _experienceToNextLevel = 100;
    private int _maxLevel = 20;
    private int _availableUpdatePoints = 0;

    private int _maxHealthLevel = 1;
    private int _speedLevel = 1;
    private int _bulletSpeedLevel = 1;
    private int _shootCooldownLevel = 1;
    private int _damageLevel = 1;
    private int _collectDistanceLevel = 1;

    private float _maxSpeed = 500f;
    private float _maxBulletSpeed = 300f;

    public float PlayerHealth
    {
        get { return _health; }
        set { _health = value; }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { if (value > 0) _maxHealth = value; }
    }

    public bool IsAlive
    {
        get { return _isAlive; }
    }

    public bool IsPaused
    {
        get { return _isPaused; } set { _isPaused = value; }
    }

    public float Speed
    {
        get { return _speed; }
        set { if (value > 0) _speed = value; }
    }

    public GameObject Bullet
    {
        get { return _bullet; }
        set { _bullet = value; }
    }

    public float BulletSpeed
    {
        get { return _bulletSpeed; }
        set { if (value > 0) _bulletSpeed = value; }
    }

    public float ShootCooldown
    {
        get { return _shootCooldown; }
        set { if (value > 0) _shootCooldown = value; }
    }

    public float PlayerDamage
    {
        get { return _damage; }
        set { if (value > 0) _damage = value; }
    }

    public Transform FirePoint2
    {
        get { return _firePoint2; }
        set { _firePoint2 = value; }
    }

    public float CollectDistance
    {
        get { return _collectDistance; }
        set { if (value > 0) _collectDistance = value; }
    }

    public float CurrentExperience
    {
        get { return _currentExperience; }
        private set { _currentExperience = value; }
    }

    public int ExperienceLevel
    {
        get { return _experienceLevel; }
        set { if (value > _experienceLevel) _experienceLevel = value; }
    }

    public float ExperienceToNextLevel
    {
        get { return _experienceToNextLevel; }
        set { if (value > _experienceToNextLevel) _experienceToNextLevel = value; }
    }

    public int AviableUpdatePoints
    {
        get { return _availableUpdatePoints; }
        set { _availableUpdatePoints = value; }
    }

    public int MaxLevel
    {
        get { return _maxLevel; }
    }

    public int MaxHealthLevel
    {
        get { return _maxHealthLevel; }
        set { if (value > 0) _maxHealthLevel = value; }
    }

    public int SpeedLevel
    {
        get { return _speedLevel; }
        set { if (value > 0) _speedLevel = value; }
    }

    public int BulletSpeedLevel
    {
        get { return _bulletSpeedLevel; }
        set { if (value > 0) _bulletSpeedLevel = value; }
    }

    public int ShootCooldownLevel
    {
        get { return _shootCooldownLevel; }
        set { if (value > 0) _shootCooldownLevel = value; }
    }

    public int DamageLevel
    {
        get { return _damageLevel; }
        set { if (value > 0) _damageLevel = value; }
    }

    public int CollectDistanceLevel
    {
        get { return _collectDistanceLevel; }
        set { if (value > 0) _collectDistanceLevel = value; }
    }

    public float MaxSpeed { get { return _maxSpeed; } }

    public float MaxBulletSpeed { get { return _maxBulletSpeed; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;

        _statisticCounter = FindObjectOfType<StatisticCounter>();

        _perfomanceImprovement = FindObjectOfType<PerformanceImprovement>();
        _userInterface = FindObjectOfType<UserInterface>();

        _updateSelectionField = FindObjectOfType<SelectionUpdate>().GetComponent<SelectionUpdate>();
        _updateSelectionField.DeactivateUpdateField();

        _maxHealth *= PlayerPrefs.GetInt("MaxHealthLevel", 1);
        _shootCooldown /= PlayerPrefs.GetInt("ShootCooldownLevel", 1);
        _damage *= PlayerPrefs.GetInt("DamageLevel", 1);
        _collectDistance *= PlayerPrefs.GetInt("CollectDistanceLevel", 1);

        _health = _maxHealth;
    }

    public void RotateToMouse()
    {
        Vector3 mousePosToPlayer = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(mousePosToPlayer.x, mousePosToPlayer.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    public void Move(float xVector, float yVector)
    {
        Vector2 targetPosition = new Vector2(xVector, yVector);

        _rb.velocity = Vector2.SmoothDamp(_rb.velocity, targetPosition * _speed * Time.fixedDeltaTime, ref currentVelocity, _movementSmooth);
    }

    public void Shoot()
    {
        if (!_canShoot) return;

        Vector3 direction = transform.up;

        GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, transform.rotation); 

        Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();

        _bulletRb.AddForce(direction * _bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        _canShoot = false;
        StartCoroutine(ShootCooldownCoroutine());
    }

    public void Shoot_DoubleGun()
    {
        if (!_canShoot) return;

        Vector3 direction = transform.up;

        GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, transform.rotation);
        GameObject bullet2 = Instantiate(_bullet, _firePoint2.transform.position, transform.rotation);

        Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();
        Rigidbody2D _bullet2Rb = bullet2.GetComponent<Rigidbody2D>();

        _bulletRb.AddForce(direction * _bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        _bullet2Rb.AddForce(direction * _bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        _canShoot = false;
        StartCoroutine(ShootCooldownCoroutine());
        
    }

    public void Shoot_MachineGun()
    {
        if (!_canShoot) return;
        
        Vector3 direction = transform.up;

        direction.x += (Random.Range(-0.5f, 0.5f));
        direction.y += (Random.Range(-0.5f, 0.5f));

        float speedMultiplier = 0.5f;

        GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, transform.rotation);

        Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();

        _bulletRb.AddForce(direction * _bulletSpeed * speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);

        _canShoot = false;
        StartCoroutine(ShootCooldownCoroutine());
        
    }

    public void Shoot_SniperGun()
    {
        if (!_canShoot) return;

        Vector3 direction = transform.up;

        float speedMultiplier = 3f;

        GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, transform.rotation);

        Rigidbody2D _bulletRb = bullet.GetComponent<Rigidbody2D>();

        _bulletRb.AddForce(direction * _bulletSpeed * speedMultiplier * Time.fixedDeltaTime, ForceMode2D.Impulse);

        _canShoot = false;
        StartCoroutine(ShootCooldownCoroutine());
        
    }

    private IEnumerator ShootCooldownCoroutine()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }

    public void ApplyDamage(float damage)
    {
        if (damage > 0 && _enableToDamage)
        {
            PlayerHealth -= damage;
            _statisticCounter.IncomingDamage += (int)damage;
            _userInterface.UpdateHealthBar();
            StartCoroutine(DamageCooldown());
            _enableToDamage = false;

            if (PlayerHealth < 0.5f)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        _isAlive = false;
        Time.timeScale = 0;

        _userInterface.ActivateGameOverMenu();

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + _statisticCounter.CollectedCoins);
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(_damageCooldown);
        _enableToDamage = true;
    }

    public void AddHealth(float value)
    {
        if (value > 0)
        {
            if (_health + value <= _maxHealth) _health += value;
            else _health = _maxHealth;

            _userInterface.UpdateHealthBar();
        }
    }

    public void AddExperience(float value)
    {
        if (value > 0)
        {
            CurrentExperience += value;
            _statisticCounter.CollectedExperiense += (int)value;
            _userInterface.UpdateExperienceBar();
        }

        while(CurrentExperience >= _experienceToNextLevel) ExperienceLevelUp();
    }

    public void AddCoin(int value)
    {
        if (value > 0)
        {
            _statisticCounter.CollectedCoins += value;
            _userInterface.UpdateCoinsText();
        }
    }

    public void ExperienceLevelUp()
    {
        CurrentExperience = CurrentExperience - ExperienceToNextLevel;
        ExperienceToNextLevel += (ExperienceToNextLevel / 100) * 10;
        ExperienceLevel++;
        if (ExperienceLevel > 30) AviableUpdatePoints += 2;
        else AviableUpdatePoints++;

        _userInterface.UpdateExperienceBar();
        _userInterface.ExperienceSliderMaxValue = ExperienceToNextLevel;

        _perfomanceImprovement.UpdateAviableUpdatePointsText();

        _userInterface.UpdatePlayerLevel();

        if (ExperienceLevel % 10 == 0)
        {
            _updateSelectionField.ActivateUpdateField();
        }
    }

    public void PauseGame()
    {
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1;
            _userInterface.DeactivatePauseMenu();
        }
        else
        {
            _isPaused = true;
            Time.timeScale = 0;
            _userInterface.ActivatePauseMenu();
        }
    }
}