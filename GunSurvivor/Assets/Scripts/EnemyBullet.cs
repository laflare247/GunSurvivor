using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void Start()
    {
        Invoke(nameof(Destroy), _lifeTime);

        _player = FindObjectOfType<CharacterController>().GetComponent<CharacterController>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterController player))
        {
            player.ApplyDamage(_bulletDamage + Random.Range(-_bulletDamage / 10, _bulletDamage / 10));
            Destroy(gameObject);
        }
    }
}
