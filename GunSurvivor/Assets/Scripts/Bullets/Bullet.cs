using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _lifeTime = 5f;

    [SerializeField] protected float _bulletDamage = 5f;

    private int _burnTimes = 3;

    protected CharacterController _player;

    public int BurnTimes
    {
        get { return _burnTimes; }
    }

    protected float BulletDamage
    {
        get { return _bulletDamage; }
        set { _bulletDamage = value; }
    }

    protected virtual void Start()
    {
        Invoke(nameof(Destroy), _lifeTime);

        _player = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();

        BulletDamage += _player.PlayerDamage;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            bool isCrit = false;

            int criticalDamageMultiplier = 1;

            int randomNumber = Random.Range(0, 21);

            if (randomNumber == 20)
            {
                criticalDamageMultiplier = 10;
                isCrit = true;
            }

            enemy.ApplyDamage((_bulletDamage + Random.Range(-_bulletDamage / 10, _bulletDamage / 10)) * criticalDamageMultiplier, isCrit);
            Destroy(gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
