using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : EnemyScript
{
    public float WEB_RANGE = 5f;
    public float WEB_RATE_S = 3;
    public float WEB_DURATION_S = 5;

    public GameObject webShotEffectObject;

    private float webCooldown = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        webCooldown -= Time.deltaTime;
        if (webCooldown <= 0)
        {
            if (shootWeb())
            {
                webCooldown = WEB_RATE_S;
            }
        }
    }

    private bool shootWeb()
    {
        float rangeSquared = WEB_RANGE * WEB_RANGE;
        float closestDistanceSquared = float.MaxValue;
        TowerScript closestTower = null;

        // Find the closest non-webbed tower in range
        foreach (TowerScript tower in TowerManager.Instance.Towers)
        {
            float distSquared = Vector3.SqrMagnitude(tower.transform.position - transform.position);
            if ((distSquared <= rangeSquared) &&
                (distSquared < closestDistanceSquared) &&
                !tower.IsWebbed)
            {
                closestTower = tower;
                closestDistanceSquared = distSquared;
            }
        }

        if (closestTower == null) return false;

        WebShotEffectScript webShot = Instantiate(webShotEffectObject).GetComponent<WebShotEffectScript>();
        webShot.transform.position = transform.position;
        webShot.setTravel(transform.position, closestTower.transform.position);

        closestTower.Web(WEB_DURATION_S, webShot.DURATION);

        return true;
    }
}
