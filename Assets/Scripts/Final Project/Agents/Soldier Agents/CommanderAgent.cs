using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CommanderAgent", menuName = "AgentTypes/CommanderAgent")]
public class CommanderAgent : AgentType
{
    [SerializeField]
    private float blinkRange;

    [SerializeField]
    private float blinkCooldown;

    public float BlinkRange { get { return blinkRange; } }
    public float BlinkCooldown { get { return blinkCooldown; } }

    public override void UseAbility(SoldierAgent agent, int abilityLevel)
    {
        throw new System.NotImplementedException();
    }
}
