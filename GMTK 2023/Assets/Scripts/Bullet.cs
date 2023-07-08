using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject hitEffect;

    private void Update()
    {
        // delete once it has gone out of view
        if(transform.position.magnitude > 15) { // assumes camera is centered
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction) {
        GetComponent<Rigidbody2D>().velocity = speed * direction.normalized;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
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
