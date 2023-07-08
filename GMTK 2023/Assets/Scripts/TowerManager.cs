using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject webbedEffectObject;

    public static GameObject WebbedEffectObject {  get { return instance.webbedEffectObject; } }

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
