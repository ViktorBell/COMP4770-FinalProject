using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CommanderAgent", menuName = "AgentTypes/CommanderAgent")]
public class CommanderAgent : AgentType
{
    [SerializeField]
    private float reconRange;

    [SerializeField]
    private float reconCooldown;

    public enum CommanderCommand
    {
        PermissionGranted,
        PermissionDenied,
        MoveToLocation,
        AttackEnemy,
        DefendArea,
        Regroup,
        RegroupALL,
        UseAbility,
        FindNearestCover,
        HealTeammate,
        GivePrimaryAmmo,
        GiveSecondaryAmmo,
        GiveMedit,
        GiveStim,
        GiveSpeedStim,
        GiveDamageStim,
        GiveDefenseStim,
        ScavengeGear,
        ScavengeTech,
        ScavengeWeapons,
        SwapTech,
        SwapWeapons,
        SwapExplosives,
        FormSubGroup,
        
        // Add other commands as needed
    }

    public float droneSurveillanceRadius { get { return reconRange; } }
    public float droneReconCooldown { get { return reconCooldown; } }

    public override void UseAbility(SoldierAgent agent, int abilityLevel)
    {
        // Only execute the ability if it's a commander
        if (agent.AgentTypes.Contains(this))
        {
            // Find all nodes within the recon range
            Collider[] nodeColliders = Physics.OverlapSphere(agent.transform.position, reconRange);
            foreach (Collider col in nodeColliders)
            {
                Node node = col.GetComponent<Node>();
                if (node != null && !agent.TeamBlackboard.DiscoveredNodes.Contains(node))
                {
                    agent.TeamBlackboard.DiscoveredNodes.Add(node);
                }
            }

            // Find all enemies within the recon range
            Collider[] enemyColliders = Physics.OverlapSphere(agent.transform.position, reconRange);
            foreach (Collider col in enemyColliders)
            {
                EnemyCreature enemy = col.GetComponent<EnemyCreature>();
                if (enemy != null && !agent.TeamBlackboard.TaggedEnemyData.Contains(col.transform.position))
                {
                    agent.TeamBlackboard.TaggedEnemyData.Add(col.transform.position);
                }
            }
        }
    }
}
