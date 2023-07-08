using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float duration;

    void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            enemy.TakeDamage(damage);
        }
    }
}
