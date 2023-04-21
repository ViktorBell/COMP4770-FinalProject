
/* A Stim Injection that Boosts the SoldierAgent's currentspeed for a duration and then resets it and is destroyed (one-time use consumable)*/
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedStimInjection", menuName = "Gear/SpeedStimInjection")]
public class SpeedStimInjection : StimInjection
{
    private float originalSpeed;
    
    protected override void BoostStat(SoldierAgent agent)
    {
        originalSpeed = agent.currentSpeed;
        agent.currentSpeed *= (1 + boostPercentage);
    }

    protected override void ResetStat(SoldierAgent agent)
    {
        agent.currentSpeed = originalSpeed;
    }

}
