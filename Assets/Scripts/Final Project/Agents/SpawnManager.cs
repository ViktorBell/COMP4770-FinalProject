using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    // Prefabs
    public GameObject soldierAgentPrefab;
    public GameObject enemyCreaturePrefab;

    // Spawn points
    public Transform[] soldierAgentSpawnPoints;
    public Transform[] enemyCreatureSpawnPoints;

    // Weapon, armor, and gear lists
    public List<ProjectWeapon> availablePrimaryWeapons;
    public List<ProjectWeapon> availableSecondaryWeapons;
    public List<ProjectWeapon> availableMeleeWeapons;
    public List<Gear> availableExplosives;
    public List<Gear> availableMedkits;
    public List<Gear> availableStimInjections;

    // Agent type list (excluding Commander)
    public List<AgentType> availableAgentTypes;
    public CommanderAgent commanderAgentType;

    // Team settings
    public int numberOfTeams = 4;
    public int minTeamSize = 4;
    public int maxTeamSize = 12;
    public int defaultTeamSize = 8;

    public float enemySpawnInterval = 5f;
    public float soldierAgentCheckRadius = 5f;

    

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
       
        SpawnSoldierAgents(numberOfTeams, defaultTeamSize);
        StartCoroutine(SpawnEnemyCreatures());
    }

    private IEnumerator SpawnEnemyCreatures()
    {
        while (true)
        {
            foreach (Transform spawnPoint in enemyCreatureSpawnPoints)
            {
                Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, soldierAgentCheckRadius);
                bool hasSoldierAgent = colliders.Any(col => col.GetComponent<SoldierAgent>());

                if (!hasSoldierAgent)
                {
                    Instantiate(enemyCreaturePrefab, spawnPoint.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }


    private void SpawnSoldierAgents(int numTeams, int teamSize)
    {
        for (int i = 0; i < numTeams; i++)
        {
            List<SoldierAgent> teamMembers = new List<SoldierAgent>();

            for (int j = 0; j < teamSize; j++)
            {
                // Instantiate a new SoldierAgent
                GameObject newAgentObject = Instantiate(soldierAgentPrefab, soldierAgentSpawnPoints[j].position, Quaternion.identity);
                SoldierAgent newAgent = newAgentObject.GetComponent<SoldierAgent>();

                // Assign weapons, armor, and gear
                newAgent.AssignRandomLoadout();
                newAgent.teamNumber = i;

                // Add the new agent to the team list
                teamMembers.Add(newAgent);
            }

            // Assign Agent Types
            AssignAgentTypes(teamMembers);

            // Assign Commander
            AssignCommander(teamMembers);
        }
    }

    private void AssignCommander(List<SoldierAgent> teamMembers)
    {
        SoldierAgent commander = null;
        float highestScore = -1f;

        foreach (SoldierAgent agent in teamMembers)
        {
            float agentScore = (0.5f * agent.luck) + (0.3f * agent.courage) + (0.2f * agent.empathy);

            if (agentScore > highestScore)
            {
                highestScore = agentScore;
                commander = agent;
            }
        }

        // You can now assign the commander role to the SoldierAgent instance with the highest score
        // This can be done by setting a property, calling a method, or any other way you have implemented the commander role
    }


    private void AssignAgentTypes(List<SoldierAgent> teamMembers)
    {
        int teamSize = teamMembers.Count;

        if (teamSize < defaultTeamSize)
        {
            // Randomly assign agent types with no duplicates
            List<AgentType> shuffledAgentTypes = availableAgentTypes.OrderBy(a => Random.value).ToList();
            for (int i = 0; i < teamSize; i++)
            {
                teamMembers[i].AssignAgentType(shuffledAgentTypes[i]);
            }
        }
        else
        {
            // Assign one of each type to the first 8 agents
            for (int i = 0; i < defaultTeamSize; i++)
            {
                teamMembers[i].AssignAgentType(availableAgentTypes[i]);
            }

            // Randomly assign the rest of the agent types
            for (int i = defaultTeamSize; i < teamSize; i++)
            {
                teamMembers[i].AssignAgentType(availableAgentTypes[Random.Range(0, availableAgentTypes.Count)]);
            }
        }
    }





}

