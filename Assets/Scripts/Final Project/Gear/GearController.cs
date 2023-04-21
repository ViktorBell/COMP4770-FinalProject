using UnityEngine;

public class GearController : MonoBehaviour
{
    [SerializeField]
    private Gear gear;

    private bool isUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        SoldierAgent agent = other.GetComponent<SoldierAgent>();

        if (agent != null && !isUsed)
        {
            if (agent.gear.Count < agent.totalGearCap)
            {

                agent.gear.Add(gear);

            }
            else { 
            
                // Add in logic for  the agent to choose to pickup the gear or mark the location for his teammates so if it is something one of them really needs they can decide to go get it by requesting permission from the commander
            }
        }
    }

    /* Idea for how to manage upgrade visuals for gear:
     *
     * store the references to the different meshes directly in the GearController script. You could have a list of meshes or prefabs that represent different variations of the gear for various levels or upgrades. 
     * The GearController script would be responsible for changing the mesh or visual representation based on the current level or upgrade status of the gear
     */

}
