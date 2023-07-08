using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerScript : EnemyScript
{
    public float HEAL_RATE_S = 3;
    public int HEAL_AMOUNT = 1;
    public float HEAL_RADIUS = 5;

    private float healBurstCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healBurstCooldown -= Time.deltaTime;
        while (healBurstCooldown <= 0)
        {
            healBurst();
            healBurstCooldown += HEAL_RATE_S;
        }
    }

    private void healBurst()
    {
        float radSquared = HEAL_RADIUS * HEAL_RADIUS;
        foreach (GameObject enemy in EnemyManager.Instance.Enemies)
        {
            if (enemy == gameObject) continue;

            float distSquared = Vector3.SqrMagnitude(enemy.transform.position - transform.position);
            if (distSquared <= radSquared)
            {
                EnemyScript script = enemy.GetComponent<EnemyScript>();
                script.Heal(HEAL_AMOUNT);
            }
        }
    }
}
