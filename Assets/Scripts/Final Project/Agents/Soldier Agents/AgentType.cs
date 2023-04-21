using UnityEngine;

public abstract class AgentType : ScriptableObject
{
    public GameObject[] techPrefabs; // Array of tech mesh prefabs for different upgrade levels
    public float cooldownDuration;

    public int maxLevel; // Maximum level for upgrades
    public int[] experienceThresholds; // Experience points required for each level

    // Implement the ability logic in a method
    public abstract void UseAbility(SoldierAgent agent, int abilityLevel);

    // Any other properties or methods for this agent type
}
