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

    private bool isWebbed = false;
    private float webTimer = 0;
    public bool IsWebbed { get { return isWebbed; } }

    // Start is called before the first frame update
    void Start()
    {
        TowerManager.Instance.Towers.Add(this);
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
                if (isWebbed) shotTimer += secondsPerShot;
                Shoot();
            }

            // look at the target
            Vector2 lookDir = target.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg);
        }

        if (isWebbed)
        {
            webTimer -= Time.deltaTime;
            if (webTimer <= 0) isWebbed = false;
        }
    }

    protected virtual void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Bullet>().SetDirection(target.transform.position - transform.position);
    }

    public void Web(float durationS, float animationDelayS)
    {
        isWebbed = true;
        webTimer = durationS;

        WebbedEffectScript webbedEffect = Instantiate(TowerManager.WebbedEffectObject).GetComponent<WebbedEffectScript>();
        webbedEffect.START_DELAY = animationDelayS;
        webbedEffect.setCenter(transform.position);
        webbedEffect.setEffectDuration(durationS);
    }
}
