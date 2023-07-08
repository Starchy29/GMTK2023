using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBALLTestScript : MonoBehaviour
{
    public GameObject basicEnemyObject = null;
    public PathScript path = null;
    // Start is called before the first frame update
    void Start()
    {
        if (basicEnemyObject == null)
        {
            Debug.LogError("Basic enemy prefab not set.");
            return;
        }
        
        if (path == null)
        {
            Debug.LogError("Level path not set.");
            return;
        }

        if (path.startingPoint == null)
        {
            Debug.LogError("Level path start not set.");
            return;
        }

        EnemyScript newEnemy = Instantiate(basicEnemyObject).GetComponent<EnemyScript>();
        newEnemy.setPath(path.startingPoint);
        Vector3 enemyStart = path.startingPoint.transform.position;
        enemyStart.z = -1;
        newEnemy.transform.position = enemyStart;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
