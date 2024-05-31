using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpBonus : Bonus
{
    public override void ApplyEffect(PlayerController player)
    {
        player.EnableDoubleJump();      
    }

    public override void RemoveEffect(PlayerController player)
    {    
         player.DisableDoubleJump();       
    }

    
}
