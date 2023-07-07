using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int speed;

    private PathPointScript targetPoint = null;
    private float TARGET_CLOSENESS = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.Instance.AddEnemy(gameObject);
    }

    // Update is called once per frame
    void Update()
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

    private void move(float time)
    {
        // Sanity checks
        if (targetPoint.nextPoint == null) return;
        if (health <= 0) return;

        // Handle moving to next target point, or the end of the path
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.transform.position);
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
        Vector3 direction = targetPoint.transform.position - transform.position;
        direction.Normalize();

        // Do not move past the target
        float movementMagnitude = speed * time;
        if (movementMagnitude > distanceToTarget)
        {
            movementMagnitude = distanceToTarget;
        }

        transform.position += direction * movementMagnitude;
        transform.rotation = Quaternion.Euler(direction);
    }

    private void score()
    {
        // TODO - Code for when the enemy reaches the end
    }
}
