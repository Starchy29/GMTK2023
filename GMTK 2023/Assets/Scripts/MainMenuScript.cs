using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject pathPointObject;
    public GameObject basicEnemyObject;
    public GameObject fastEnemyObject;
    public GameObject tankEnemyObject;
    public GameObject healerEnemyObject;
    public GameObject spiderObject;
    public float SPAWN_RATE_S = 1;

    private float spawnTimer = 0;

    private GameObject[] randomEnemies;

    // Start is called before the first frame update
    void Start()
    {
        randomEnemies = new GameObject[]
        {
            basicEnemyObject,
            fastEnemyObject,
            tankEnemyObject,
            healerEnemyObject,
            spiderObject
        };
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        while (spawnTimer <= 0)
        {
            spawnRandom();
            spawnTimer += SPAWN_RATE_S;
        }
    }

    private Vector3 randomOutsidePoint()
    {
        float aspect = (float)Screen.width / Screen.height;
        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight * aspect;

        float minX = -(worldWidth / 2) - 1;
        float maxX = -minX;
        float minY = -(worldHeight / 2) - 1;
        float maxY = -minY;

        if (Random.value >= 0.5f)
        {
            int flip = Random.value >= 0.5f ? 1 : -1;
            // Get a random start and end point outside the screen
           return new Vector3(
                Random.Range(minX, maxX),
                flip * maxY,
                0
            );
        }
        else
        {
            int flip = Random.value >= 0.5f ? 1 : -1;
            // Get a random start and end point outside the screen
            return new Vector3(
                 flip * maxX,
                 Random.Range(minY, maxY),
                 0
             );
        }
    }

    private void spawnRandom()
    {
        // Get a random enemy type
        GameObject randomEnemy = randomEnemies[Mathf.RoundToInt(Random.Range(0, randomEnemies.Length))];
        Vector3 start = randomOutsidePoint();
        Vector3 end = randomOutsidePoint();

        PathPointScript pathPoint = Instantiate(pathPointObject).GetComponent<PathPointScript>();
        pathPoint.transform.position = end;

        EnemyScript enemy = Instantiate(randomEnemy).GetComponent<EnemyScript>();
        enemy.setPath(pathPoint);
        enemy.transform.position = start;
        enemy.specialMenuGuy();
    }
}
