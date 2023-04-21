using UnityEngine;

[CreateAssetMenu(fileName = "New Medkit", menuName = "Gear/Medkit")]
public class Medkit : Gear
{
    public float healingAmount;

    public override void Use(SoldierAgent soldier, GameObject medkitGear)
    {
        // Implement healing logic
        soldier.currenthealth += healingAmount;

        if (soldier.currenthealth > soldier.maxHealth) {
            soldier.currenthealth = soldier.maxHealth;
        }
    }
}
