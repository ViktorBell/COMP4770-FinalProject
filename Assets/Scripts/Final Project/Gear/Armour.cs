[System.Serializable]
public class Armour
{
    public enum ArmourType { Light, Medium, Heavy }

    public string armorName;
    public ArmourType type;
    public float defenseValue;
    public float maxDurability;
    public float currentDurability;
    public float speedModifier;

    public Armour(ArmourType armorType)
    {
        type = armorType;

        switch (armorType)
        {
            case ArmourType.Light:
                armorName = "Light Armor";
                defenseValue = 10; // Example value, adjust as needed
                maxDurability = 100; // Example value, adjust as needed
                speedModifier = 1.0f; // No speed impact
                break;

            case ArmourType.Medium:
                armorName = "Medium Armor";
                defenseValue = 20; // Example value, adjust as needed
                maxDurability = 150; // Example value, adjust as needed
                speedModifier = 0.9f; // 10% speed reduction
                break;

            case ArmourType.Heavy:
                armorName = "Heavy Armor";
                defenseValue = 30; // Example value, adjust as needed
                maxDurability = 200; // Example value, adjust as needed
                speedModifier = 0.8f; // 20% speed reduction
                break;
        }

        currentDurability = maxDurability;
    }

    // You can add methods specific to Armor if needed
}
