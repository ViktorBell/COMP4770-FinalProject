using UnityEngine;

[System.Serializable]
public class AbilityState
{
    // 'AgentType' dictates the 'tech' a SoldierAgent gets, and the tech(just the aesthetic) 'provides' the ability (ability functionality is actually encoded in the AgentType) this results in the AgentType being synonymous with the abilityType
    public AgentType abilityType;
    public float cooldownTimeRemaining;
    public float maxCooldownTime;
    public GameObject techInstance;
    public int currentLevel;
    public int experiencePoints;

    public void UpdateCooldownTime() 
    {
        if (cooldownTimeRemaining > 0)
        {
            cooldownTimeRemaining -= Time.deltaTime;
        }
    }
}
