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

        transform.position += new Vector3(direction.x, direction.y, 0) * movementMagnitude;

        float vectorAngle = Mathf.Rad2Deg * Mathf.Atan(direction.x / direction.y);
        Debug.Log(vectorAngle);
        transform.rotation = Quaternion.AngleAxis(vectorAngle, new Vector3(0,0,-1));
    }

    private void score()
    {
        // TODO - Code for when the enemy reaches the end
        Debug.Log("SCORE");
        Destroy(gameObject);
    }
}
