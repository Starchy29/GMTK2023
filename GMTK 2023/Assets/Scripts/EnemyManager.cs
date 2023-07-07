using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies;

    private static EnemyManager instance;
    public static EnemyManager Instance { get; set; }

    void Awake()
    {
        enemies = new List<GameObject>();
        instance = this;
    }

    public void AddEnemy(GameObject enemy) {
        enemies.Add(enemy);
    }
}
