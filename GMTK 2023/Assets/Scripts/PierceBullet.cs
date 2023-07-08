using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceBullet : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if(enemy != null) {
            enemy.GetComponent<EnemyScript>().TakeDamage(damage);
            if(hitEffect != null) {
                GameObject effect = Instantiate(hitEffect);
                effect.transform.position = transform.position;
            }
        }
    }
}
