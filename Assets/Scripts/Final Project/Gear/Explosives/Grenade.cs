using UnityEngine;

[CreateAssetMenu(fileName = "New Grenade", menuName = "Gear/Grenade")]
public class Grenade : Gear
{
    public float explosionRadius;
    public float explosionDamage;

    public override void Use(SoldierAgent soldier, GameObject grenadeGear)
    {
        // Implement grenade throwing and explosion logic
    }
}
