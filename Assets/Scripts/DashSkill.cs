using System;
using UnityEngine;

[CreateAssetMenu]
public class DashSkill : Ability
{
    [SerializeField] private float dashSpeed;
    private CharacterMovement _characterMovement;
    private CharacterAnimator _characterAnimator;
    private static readonly int Dash = Animator.StringToHash("Dash");
    
    public override void ActivateSkill(GameObject parent)
    {
        if (_characterMovement == null || _characterAnimator == null)
        {
            Func<CharacterMovement> component = parent.GetComponent<CharacterMovement>;
            _characterMovement = component();
            Func<CharacterAnimator> func = parent.GetComponent<CharacterAnimator>;
            _characterAnimator = func();
        }

        if (_characterMovement is null || _characterAnimator is null) return;
        if (!(_characterMovement.CurrentSpeed > 0.9f)) return;
        _characterMovement.MoveSpeed = dashSpeed;
        _characterAnimator.Animator.SetTrigger(Dash);
    }

    public override void DisableSkill(GameObject parent)
    {
        if (_characterMovement is not null)
        {
            _characterMovement.MoveSpeed = _characterMovement.BaseMoveSpeed;
        }
    }
}
