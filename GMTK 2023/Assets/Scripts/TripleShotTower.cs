using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotTower : TowerScript
{
    protected override void Shoot()
    {
        Vector2 straightShot = (target.transform.position - transform.position).normalized;
        Vector2 perp = new Vector2(straightShot.y, -straightShot.x) * 0.4f;
        Vector2[] directions = new Vector2[3] {
            straightShot,
            straightShot + perp,
            straightShot - perp
        };

        foreach(Vector2 direction in directions) {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().SetDirection(direction);
        }
    }
}
