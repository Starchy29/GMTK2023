using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int cost;

    public int Cost { get { return cost; } }

    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.Instance.AddEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            EnemyManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }
}
