using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int scoresLeft;
    [SerializeField] private int currency;
    [SerializeField] private GameObject commonEnemy;
    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject tankEnemy;
    [SerializeField] private TMPro.TextMeshProUGUI currencyLabel;
    [SerializeField] private TMPro.TextMeshProUGUI scoreLabel;

    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> spawningEnemies = new Queue<GameObject>();

    private const float SECONDS_PER_SPAWN = 0.5f;
    private const float SPAWN_COOLDOWN = 3.0f;
    private float squadTimer;
    private float spawnCooldown;

    void Awake()
    {
        enemies = new List<GameObject>();
        instance = this;
        currencyLabel.text = "$" + currency;
        scoreLabel.text = "Goal: " + scoresLeft;
    }

    void Update() {
        if(spawningEnemies.Count > 0) {
            squadTimer -= Time.deltaTime;
            if(squadTimer <= 0) {
                squadTimer += SECONDS_PER_SPAWN;
                Instantiate(spawningEnemies.Dequeue()); // automatically places self at path start
            }
        }

        if(spawnCooldown > 0) {
            spawnCooldown -= Time.deltaTime;
        }
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }

    public void Score() {
        scoresLeft--;
        scoreLabel.text = "Goal: " + scoresLeft;
        if(scoresLeft <= 0) {
            Application.Quit();
        }
    }

    // button functions
    private void BuyEnemy(GameObject prefab) {
        if(enemyQueue.Count >= 7) {
            return;
        }

        int cost = prefab.GetComponent<EnemyScript>().Cost;
        if(currency < cost) {
            return;
        }

        currency -= cost;
        currencyLabel.text = "$" + currency;
        enemyQueue.Enqueue(prefab);
    }

    public void BuyCommon() {
        BuyEnemy(commonEnemy);
    }

    public void BuyFast() {
        BuyEnemy(fastEnemy);
    }

    public void BuyTank() {
        BuyEnemy(tankEnemy);
    }

    public void SpawnWave() {
        if(spawnCooldown > 0) {
            return;
        }

        spawningEnemies = enemyQueue;
        enemyQueue = new Queue<GameObject>();
        squadTimer = 0;
        spawnCooldown = SPAWN_COOLDOWN;
    }
}
