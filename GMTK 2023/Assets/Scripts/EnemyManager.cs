using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject cockroachPic;
    [SerializeField] private GameObject antPic;
    [SerializeField] private GameObject beetlePic;
    [SerializeField] private GameObject spiderPic;
    [SerializeField] private GameObject ladybugPic;
    private List<GameObject> queuePics = new List<GameObject>();

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
                GameObject spawn = Instantiate(spawningEnemies.Dequeue()); // automatically places self at path start
                enemies.Add(spawn);
            }
        }

        if(spawnCooldown > 0) {
            spawnCooldown -= Time.deltaTime;
        }
        else if(enemyQueue.Count > 0) {
            spawnButton.interactable = true;
        }

        // check for win and lose
        if(currency <= 0 && enemies.Count <= 0 && enemyQueue.Count <= 0 && spawningEnemies.Count <= 0 && scoresLeft > 0) {
            // loss
            transform.Find("Lose Menu").gameObject.SetActive(true);
        }
        else if(scoresLeft <= 0) {
            transform.Find("Win Menu").gameObject.SetActive(true);
        }
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }

    public void Score() {
        if(scoresLeft <= 0) {
            return;
        }

        scoresLeft--;
        scoreLabel.text = "Goal: " + scoresLeft;
    }

    private void UpdateBuyEnabled() {
        foreach(Button button in buyButtons) {
            button.interactable = enemyQueue.Count < MAX_PER_WAVE && int.Parse(button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text[1] + "") <= currency;
        }
    }

    // button functions
    private void BuyEnemy(GameObject prefab, GameObject pic) {
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

        GameObject newPic = Instantiate(pic, transform);
        newPic.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30 + 50 * queuePics.Count, -135);
        queuePics.Add(newPic);
    }

    public void BuyCommon() {
        BuyEnemy(commonEnemy, cockroachPic);
    }

    public void BuyFast() {
        BuyEnemy(fastEnemy, antPic);
    }

    public void BuyTank() {
        BuyEnemy(tankEnemy, beetlePic);
    }

    public void BuyHealer()
    {
        BuyEnemy(healerEnemy, ladybugPic);
    }

    public void BuySpider()
    {
        BuyEnemy(spiderEnemy, spiderPic);
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

        foreach(GameObject pic in queuePics) {
            Destroy(pic);
        }
        queuePics = new List<GameObject>();
    }

    public void MenuButton() {
        SceneManager.LoadScene(0);
    }

    public void NextButton() {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene >= SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void RetryButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
