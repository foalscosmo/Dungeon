using System;
using UnityEngine;

public class DashSkill : Ability
{
   public override void Activate()
   {
      Debug.Log("shemovedi");
   }

   public override void Deactivate()
   {
      Debug.Log("gavedi");
   }
}
