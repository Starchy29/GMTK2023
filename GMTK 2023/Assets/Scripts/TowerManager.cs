using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private List<TowerScript> towers;
    public List<TowerScript> Towers { get { return towers; } }

    private static TowerManager instance;
    public static TowerManager Instance { get { return instance; } }

    void Awake()
    {
        towers = new List<TowerScript>();
        instance = this;
    }
}
