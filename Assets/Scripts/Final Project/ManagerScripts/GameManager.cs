using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private SpawnManager spawnManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the simulation by spawning SoldierAgents and EnemyCreatures
        spawnManager.SpawnSoldierAgentTeams(1,2);
        spawnManager.StartSpawningEnemyCreatures();
    }
}
