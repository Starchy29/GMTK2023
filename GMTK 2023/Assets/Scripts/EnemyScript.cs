using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int speed;
    [SerializeField] private int cost;

    public int Cost { get { return cost; } }
    
    private PathPointScript targetPoint = null;
    private float TARGET_CLOSENESS = 0.01f;

    private int maxHealth = 0;
    protected bool isSpecialMenuGuy = false;

    // Start is called before the first frame update
    protected void Start()
    {
        // always start at the start of the path
        if (targetPoint == null)
        {
            targetPoint = GameObject.Find("Path").GetComponent<PathScript>().startingPoint;
            transform.position = targetPoint.transform.position;
        }

        maxHealth = health;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (targetPoint != null)
        {
            move(Time.deltaTime);
        }
    }

    public void setPath(PathPointScript startingPoint)
    {
        targetPoint = startingPoint;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            EnemyManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        HealingEffect healEffect = Instantiate(EnemyManager.HealingEffectObject).GetComponent<HealingEffect>();
        healEffect.transform.parent = transform;
    }

    private void move(float time)
    {
        // Sanity checks
        if (targetPoint == null) return;
        if (health <= 0) return;

        // Handle moving to next target point, or the end of the path
        float distanceToTarget = Vector2.Distance(transform.position, targetPoint.transform.position);
        if (distanceToTarget < TARGET_CLOSENESS)
        {
            if (targetPoint.nextPoint == null)
            {
                score();
                targetPoint = null;
                return;
            }
            targetPoint = targetPoint.nextPoint;
        }

        // Continue walking towards the next path point
        Vector2 direction = targetPoint.transform.position - transform.position;
        direction.Normalize();

        // Do not move past the target
        float movementMagnitude = speed * time;
        if (movementMagnitude > distanceToTarget)
        {
            movementMagnitude = distanceToTarget;
        }

        // Move towards next point
        transform.position += new Vector3(direction.x, direction.y, 0) * movementMagnitude;

        // Rotate towards next point
        float vectorAngle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, vectorAngle);
    }

    private void score()
    {
        EnemyManager.Instance.Score();
        EnemyManager.Instance.RemoveEnemy(gameObject);
        Destroy(gameObject);

        if (isSpecialMenuGuy)
        {
            Destroy(targetPoint.gameObject);
        }
    }

    public void specialMenuGuy()
    {
        isSpecialMenuGuy = true;
    }
}
