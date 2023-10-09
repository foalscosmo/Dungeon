using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private AbilityStats abilityStats;
    public float CoolDownTime => abilityStats.CoolDownTime;
    public float ActiveTime => abilityStats.ActiveTime;

    public string Name => abilityStats.Name;

 

    public virtual void Activate(){}
   
    public virtual void Deactivate(){}
}