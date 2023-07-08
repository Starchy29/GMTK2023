using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int scoresLeft;
    [SerializeField] private int currency;
    [SerializeField] private GameObject commonEnemy;
    [SerializeField] private GameObject fastEnemy;
    [SerializeField] private GameObject tankEnemy;
    [SerializeField] private GameObject healerEnemy;
    [SerializeField] private GameObject spiderEnemy;
    [SerializeField] private TMPro.TextMeshProUGUI currencyLabel;
    [SerializeField] private TMPro.TextMeshProUGUI scoreLabel;
    [SerializeField] private Button[] buyButtons;
    [SerializeField] private Button spawnButton;

    [SerializeField] private GameObject healingEffectObject;

    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    public static GameObject HealingEffectObject { get { return instance.healingEffectObject; } }

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> spawningEnemies = new Queue<GameObject>();

    private const float SECONDS_PER_SPAWN = 0.5f;
    private const float SPAWN_COOLDOWN = 3.0f;
    private float squadTimer;
    private float spawnCooldown;

    private const int MAX_PER_WAVE = 7;

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
        else if(enemyQueue.Count > 0) {
            spawnButton.interactable = true;
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

    private void UpdateBuyEnabled() {
        foreach(Button button in buyButtons) {
            button.interactable = enemyQueue.Count < MAX_PER_WAVE && int.Parse(button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[1] + "") <= currency;
        }
    }

    // button functions
    private void BuyEnemy(GameObject prefab) {
        if(enemyQueue.Count >= MAX_PER_WAVE) {
            return;
        }

        int cost = prefab.GetComponent<EnemyScript>().Cost;
        if(currency < cost) {
            return;
        }

        currency -= cost;
        currencyLabel.text = "$" + currency;
        enemyQueue.Enqueue(prefab);

        UpdateBuyEnabled();
        if(spawnCooldown <= 0) {
            spawnButton.interactable = true;
        }
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

    public void BuyHealer()
    {
        BuyEnemy(healerEnemy);
    }

    public void BuySpider()
    {
        BuyEnemy(spiderEnemy);
    }

    public void SpawnWave() {
        if(spawnCooldown > 0) {
            return;
        }

        spawningEnemies = enemyQueue;
        enemyQueue = new Queue<GameObject>();
        squadTimer = 0;
        spawnCooldown = SPAWN_COOLDOWN;

        UpdateBuyEnabled();
        spawnButton.interactable = false;
    }
}
