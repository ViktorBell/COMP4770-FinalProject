
using System.Collections.Generic;
using UnityEngine;
using static Armour;

public class SoldierAgent : MonoBehaviour
{
    public float currenthealth;
    public float maxHealth;
    public float baseSpeed;
    public float currentSpeed;
    public float stamina;
    // Other soldier properties as needed

    public AgentType agentType;


    public List<ProjectWeapon> weapons = new List<ProjectWeapon>();
    public Armour armour;
    public List<Gear> gear = new List<Gear>();
    public List<AbilityState> abilities = new List<AbilityState>();

    /* Gear Slot Counts 
       Spawning counts = 2 Explosives, 3 Medkits, 3 Stims
     */
    public int totalGearCap = 8;

    public int primaryAmmoMax;
    public int secondaryAmmoMax;
    public int primaryAmmoCurrent;
    public int secondaryAmmoCurrent;

    public int teamNumber;

    public float loyalty;
    public float empathy;
    public float courage;
    public float luck;
    public float fearThreshold;
    public float currentFearLevel;

    public bool isCautious = true;
    public bool isCommanderDirected = true;

    private List<Message> messageQueue = new List<Message>();

    public SoldierAgentBlackboard agentBlackboard;
    public TeamBlackboard teamBlackboard;

    private void Awake()
    {
        agentBlackboard = new SoldierAgentBlackboard();
    }


    private void Start()
    {
        // Initialize the equippedArmor with an ArmorType
        armour = new Armour(Armour.ArmourType.Light);

        ArmourSpeedModification();
    }

    void Update()
    {
        // Update cooldown times for each ability
        foreach (AbilityState ability in abilities)
        {
            ability.UpdateCooldownTime();
        }

        ProcessMessage();


        // Other update logic
    }

    private void ArmourSpeedModification()
    {
        currentSpeed = baseSpeed * armour.speedModifier;
    }
    // Other methods for the SoldierAgent class

    public void SwapWeapon(ProjectWeapon currentWeapon, ProjectWeapon newWeapon)
    {
        // Get the SpawnManager instance
        SpawnManager spawnManager = SpawnManager.Instance;

        // Find the index of the current weapon in the weapons list
        int weaponIndex = weapons.IndexOf(currentWeapon);

        // If the current weapon is a melee weapon, check if the new weapon is also a melee weapon
        if (spawnManager.availableMeleeWeapons.Contains(currentWeapon))
        {
            if (spawnManager.availableMeleeWeapons.Contains(newWeapon))
            {
                // If both the current and new weapons are melee weapons, perform the swap
                weapons[weaponIndex] = newWeapon;
            }
            else
            {
                Debug.Log("Cannot swap a melee weapon with a non-melee weapon.");
            }
        }
        else
        {
            // If the current weapon is not a melee weapon, perform the swap 
            weapons[weaponIndex] = newWeapon;
        }
    }

    public void AssignRandomLoadout()
    {
        SpawnManager spawnManager = SpawnManager.Instance;

        // Assign a random primary, secondary, and melee weapon
        int primaryIndex = Random.Range(0, spawnManager.availablePrimaryWeapons.Count);
        int secondaryIndex = Random.Range(0, spawnManager.availableSecondaryWeapons.Count);
        int meleeIndex = Random.Range(0, spawnManager.availableMeleeWeapons.Count);

        weapons[0] = spawnManager.availablePrimaryWeapons[primaryIndex];
        weapons[1] = spawnManager.availableSecondaryWeapons[secondaryIndex];
        weapons[2] = spawnManager.availableMeleeWeapons[meleeIndex];

        // Assign a random armour type (Light, Medium, Heavy)
        int armourIndex = Random.Range(0, System.Enum.GetValues(typeof(Armour.ArmourType)).Length);
        this.armour = new Armour((Armour.ArmourType)armourIndex);

        loyalty = Random.Range(0.5f, 1f);
        empathy = Random.Range(0f, 1f);
        courage = Random.Range(0.2f, 1f);
        luck = Random.Range(0f, 1f);
        // Assign grenades, medkits, and stim injections
        gear.Clear();

        for (int i = 0; i < 2; i++)
        {
            int explosiveIndex = Random.Range(0, spawnManager.availableExplosives.Count);
            gear.Add(spawnManager.availableExplosives[explosiveIndex].Clone());
        }

        for (int i = 0; i < 3; i++)
        {
            int medkitIndex = Random.Range(0, spawnManager.availableMedkits.Count);
            gear.Add(spawnManager.availableMedkits[medkitIndex].Clone());
        }

        for (int i = 0; i < 3; i++)
        {
            int stimIndex = Random.Range(0, spawnManager.availableStimInjections.Count);
            gear.Add(spawnManager.availableStimInjections[stimIndex].Clone());
        }
    }

    public void AssignAgentType(AgentType newAgentType)
    {
        agentType = newAgentType;
        AddAbility(agentType);
    }

    public void AddAbility(AgentType ability)
    {
        AbilityState newAbilityState = new AbilityState
        {
            abilityType = ability,
            cooldownTimeRemaining = 0,
            maxCooldownTime = ability.cooldownDuration,
            techInstance = null, // Initialize this if needed
            currentLevel = 1,
            experiencePoints = 0
        };

        abilities.Add(newAbilityState);
    }

    public void ScavengeTech(AgentType newTech)
    {
        if (abilities.Count < 3)
        {
            // If there's room, simply add the new ability.
            AddAbility(newTech);
        }
        else
        {
            // If there are already 3 abilities, swap the oldest ability with the new one.
            SwapOldestAbility(newTech);
        }
    }

    //Should update this to make the swap a choice with a current one not necessarily the oldest one
    private void SwapOldestAbility(AgentType newAbility)
    {
        int oldestAbilityIndex = 0;
        int oldestExperiencePoints = abilities[0].experiencePoints;

        for (int i = 1; i < abilities.Count; i++)
        {
            if (abilities[i].experiencePoints < oldestExperiencePoints)
            {
                oldestAbilityIndex = i;
                oldestExperiencePoints = abilities[i].experiencePoints;
            }
        }

        abilities.RemoveAt(oldestAbilityIndex);
        AddAbility(newAbility);
    }

    public void SendMessage(Message message)
    {
        if (message.receiver != null)
        {
            message.receiver.ReceiveMessage(message);
        }
        else
        {
            Debug.LogError("Message receiver is null.");
        }
    }

    public void ReceiveMessage(Message message)
    {
        messageQueue.Add(message);
    }

    private void ProcessMessage()
    {
        if (messageQueue.Count > 0)
        {
            Message message = messageQueue[0];
            messageQueue.RemoveAt(0);

            switch (message.type)
            {
                case Message.MessageType.Command:
                    // Handle command messages
                    break;
                case Message.MessageType.Request:
                    // Handle request messages
                    break;
                case Message.MessageType.Report:
                    // Handle report messages
                    break;
                default:
                    Debug.LogError("Unknown message type.");
                    break;
            }
        }
    }



}


