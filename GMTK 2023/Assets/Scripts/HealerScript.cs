using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerScript : EnemyScript
{
    public float HEAL_RATE_S = 3;
    public int HEAL_AMOUNT = 1;
    public float HEAL_RADIUS = 5;

    public HealPulseScript healPulseObject = null;

    private float healBurstCooldown = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        healBurstCooldown -= Time.deltaTime;
        while (healBurstCooldown <= 0)
        {
            healBurst();
            healBurstCooldown += HEAL_RATE_S;
        }
    }

    private void healBurst()
    {
        if (isSpecialMenuGuy) return;

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

        // Draw animation
        if (healPulseObject != null)
        {
            HealPulseScript pulse = Instantiate(healPulseObject);
            Vector3 pulsePosition = pulse.transform.position;
            pulsePosition.x = transform.position.x;
            pulsePosition.y = transform.position.y;
            pulse.transform.position = pulsePosition;
            pulse.setEndRadius(HEAL_RADIUS);
        }
    }
}
