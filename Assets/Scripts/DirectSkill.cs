using System;
using UnityEngine;

[CreateAssetMenu]
public class DirectSkill : Ability
{
    private CharacterMovement _characterMovement;
    private CharacterAnimator _characterAnimator;
    private const float SkillMoveSpeed = 0; 
    private static readonly int Skill = Animator.StringToHash("DirectSkill");

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
        _characterMovement.MoveSpeed = SkillMoveSpeed;
        _characterAnimator.Animator.SetTrigger(Skill);
        
        var ray = new Ray(parent.transform.position, parent.transform.forward);
        var hits = Physics.RaycastAll(ray, 6);
        foreach (var hit in hits)
        {
            
        }

    }

    public override void DisableSkill(GameObject parent)
    {
        if (_characterMovement is not null)
        {
            _characterMovement.MoveSpeed = _characterMovement.BaseMoveSpeed;
        }
    }
}