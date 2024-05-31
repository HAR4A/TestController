using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostBonus : Bonus
{
    public override void ApplyEffect(PlayerController player)
    {
        player.EnableSpeedBoost();
    }

    public override void RemoveEffect(PlayerController player)
    {      
         player.DisableSpeedBoost();          
    }
    
}
