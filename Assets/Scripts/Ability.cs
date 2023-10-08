using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float coolDownTime;
    public float activeTime;

    public virtual void ActivateSkill(GameObject parent)
    {
        
    }
    
    public virtual void DisableSkill(GameObject parent)
    {
        
    }
}