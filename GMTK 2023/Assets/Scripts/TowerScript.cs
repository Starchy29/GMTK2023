using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private float range;
    [SerializeField] private float secondsPerShot;

    protected GameObject target;
    private float shotTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the target is lost
        if(target != null && Vector3.Distance(target.transform.position, transform.position) > range) {
            target = null;
            shotTimer = secondsPerShot;
        }

        if(target == null) {
            // check for a target
            GameObject closestEnemy = null;
            float closestDistance = 0;
            foreach(GameObject enemy in EnemyManager.Instance.Enemies) {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                if(closestEnemy == null || distance < closestDistance) {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }
            }

            target = closestEnemy;
        } else {
            // shoot at the target
            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0) {
                shotTimer += secondsPerShot;
                Shoot();
            }
        }
    }

    protected virtual void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().SetDirection(target.transform.position - transform.position);
    }
}
