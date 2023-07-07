using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private GameObject commonEnemy;

    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }

    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    void Awake()
    {
        enemies = new List<GameObject>();
        instance = this;
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy) {
        enemies.Remove(enemy);
    }

    public void SpawnCommon() {
        SpawnEnemy(commonEnemy);
    }

    private void SpawnEnemy(GameObject prefab) {
        int cost = prefab.GetComponent<EnemyScript>().Cost;
        if(currency < cost) {
            return;
        }

        currency -= cost;
        GameObject enemy = Instantiate(prefab);
        // enemy.transform.position = path start
    }
}
