using UnityEngine;

public class Bullet_Fire : Bullet
{
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
            enemy.Burn();
            Destroy(gameObject);
        }
    }
}
