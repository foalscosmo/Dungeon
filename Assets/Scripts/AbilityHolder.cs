using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private enum AbilityState
    {
        Ready,
        Active,
        CoolDown
    }

    [SerializeField] private Ability ability;
    private float _coolDownTime;
    private float _activeTime;
    [SerializeField] private KeyCode key;

    [SerializeField] private AbilityState state = AbilityState.Ready;
    
    private void Update()
    {
        switch (state)
        {
            case AbilityState.Ready:
                if (Input.GetKeyDown(key))
                {
                    ability.Activate();
                    state = AbilityState.Active;
                    _activeTime = ability.ActiveTime;
                }
                break;
            case AbilityState.Active:
                if (_activeTime > 0) _activeTime -= Time.deltaTime;
                else
                {
                    ability.Deactivate();
                    state = AbilityState.CoolDown;
                    _coolDownTime = ability.CoolDownTime;
                }
                break;
            case AbilityState.CoolDown:
                if (_coolDownTime > 0) _coolDownTime -= Time.deltaTime;
                else state = AbilityState.Ready;
                break;
        }
    }
}