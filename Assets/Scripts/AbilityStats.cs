using UnityEngine;

[CreateAssetMenu(menuName = "Create AbilityStats", fileName = "AbilityStats", order = 0)]
public class AbilityStats : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private float coolDownTime;
    [SerializeField] private float activeTime;
    public string Name => name;
    public float CoolDownTime => coolDownTime;
    public float ActiveTime => activeTime;
}