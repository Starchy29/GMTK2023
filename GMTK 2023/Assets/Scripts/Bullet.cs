using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private GameObject hitEffect;

    public void SetDirection(Vector2 direction) {
        GetComponent<Rigidbody2D>().velocity = speed * direction;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if(enemy != null) {
            enemy.GetComponent<EnemyScript>().TakeDamage(damage);
            if(hitEffect != null) {
                GameObject effect = Instantiate(hitEffect);
                effect.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}
