using UnityEngine;

public abstract class Gear : ScriptableObject
{
    public string gearName;
    public Sprite gearIcon;

    public virtual Gear Clone()
    {
        Gear gearClone = Instantiate(this);
        gearClone.name = this.gearName;
        return gearClone;
    }

    // Implement the effect of the gear in the derived classes
    public abstract void Use(SoldierAgent soldier, GameObject gearObject);
}
