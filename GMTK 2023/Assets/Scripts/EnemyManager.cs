using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private GameObject commonEnemy;
    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject tankEnemy;

    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> spawningEnemies = new Queue<GameObject>();

    private const float SECONDS_PER_SPAWN = 0.5f;
    private const float SPAWN_INTERVAL = 5.0f;
    private float squadTimer;
    private float secondsUntilSpwans;

    void Awake()
    {
        enemies = new List<GameObject>();
        instance = this;
    }

    void Update() {
        secondsUntilSpwans -= Time.deltaTime;
        if(secondsUntilSpwans <= 0) {
            secondsUntilSpwans += SPAWN_INTERVAL;
            spawningEnemies = enemyQueue;
            enemyQueue = new Queue<GameObject>();
            squadTimer = 0;
        }

        if(spawningEnemies.Count > 0) {
            squadTimer -= Time.deltaTime;
            if(squadTimer <= 0) {
                squadTimer += SECONDS_PER_SPAWN;
                SpawnEnemy(spawningEnemies.Dequeue());
            }
        }
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }

    private void SpawnEnemy(GameObject prefab) {
        GameObject enemy = Instantiate(prefab);
        // enemy.transform.position = path start;
    }

    private void BuyEnemy(GameObject prefab) {
        if(enemyQueue.Count >= 5) {
            return;
        }

        int cost = prefab.GetComponent<EnemyScript>().Cost;
        if(currency < cost) {
            return;
        }

        currency -= cost;
        enemyQueue.Enqueue(prefab);
    }

    // button functions
    public void BuyCommon() {
        BuyEnemy(commonEnemy);
    }

    public void BuyFast() {
        BuyEnemy(fastEnemy);
    }

    public void BuyTank() {
        BuyEnemy(tankEnemy);
    }
}
