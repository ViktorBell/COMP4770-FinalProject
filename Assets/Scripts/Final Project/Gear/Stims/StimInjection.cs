using System.Collections;
using UnityEngine;

public abstract class StimInjection : Gear
{
    public float boostPercentage;
    public float duration;
    public override void Use(SoldierAgent agent, GameObject gearObject)
    {
        StimCoroutineHandler.Instance.StartCoroutine(ApplyStim(agent, gearObject));
    }

    protected abstract void BoostStat(SoldierAgent agent);

    protected abstract void ResetStat(SoldierAgent agent);

    private IEnumerator ApplyStim(SoldierAgent agent, GameObject gearObject)
    {
        BoostStat(agent);
        yield return new WaitForSeconds(duration);
        ResetStat(agent);
        Destroy(gearObject);
    }

    /* May come in handy if I want to make the stims multi-use but enforce a cooldown timer before subsequent uses
    private IEnumerator Cooldown()
    {
        // Implement your cooldown logic here
        yield return new WaitForSeconds(cooldownDuration);
        // Reset the cooldown
    }
    */
}
