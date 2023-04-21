using UnityEngine;

[CreateAssetMenu(fileName = "BlinkAgentType", menuName = "AgentTypes/BlinkAgentType")]
public class BlinkAgent : AgentType
{
    [SerializeField]
    private float blinkRange;

    [SerializeField]
    private float blinkCooldown;

    public float BlinkRange { get { return blinkRange; } }
    public float BlinkCooldown { get { return blinkCooldown; } }

    public override void UseAbility(SoldierAgent agent, int abilityLevel)
    {
        // Get the blink destination based on the agent's current node and the desired behavior
        Vector3 blinkDestination = CalculateBlinkDestination(agent);

        // Check if the blink destination is valid (not out of range and has line of sight)
        if (IsBlinkValid(agent.transform.position, blinkDestination))
        {
            // Perform the blink
            agent.transform.position = blinkDestination;
        }
    }

    private Vector3 CalculateBlinkDestination(SoldierAgent agent)
    {
        // Calculate the blink destination based on the agent's current node and desired behavior
        Vector3 blinkDestination = new Vector3();
        

        return blinkDestination;
    }

    private bool IsBlinkValid(Vector3 origin, Vector3 destination)
    {
        // Check if the distance between the origin and destination is within the blink range
        if (Vector3.Distance(origin, destination) > blinkRange)
        {
            return false;
        }

        // Perform a line of sight check between the origin and destination
        RaycastHit hit;
        if (Physics.Linecast(origin, destination, out hit))
        {
            // There is an obstacle between the origin and destination
            return false;
        }

        // The blink is valid
        return true;
    }
}
