using System.Collections;
using UnityEngine;

public class Bullet_Blast : Bullet
{
    private float _blastRadius = 8f;
    private float _timeToExplode = 6f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitForExplode());
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
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

            enemy.ApplyDamage((BulletDamage + Random.Range(-BulletDamage / 10, BulletDamage / 10)) * criticalDamageMultiplier, isCrit);
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.ApplyDamage(BulletDamage);
        }
    }

    private void Explode()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius *= _blastRadius;
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }

    private IEnumerator WaitForExplode()
    {
        yield return new WaitForSeconds(_timeToExplode);
        Explode();
    }
}
