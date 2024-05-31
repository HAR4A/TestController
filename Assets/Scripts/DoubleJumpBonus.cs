using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpBonus : Bonus
{
    private bool _active = false;

    public override void ApplyEffect(PlayerController player)
    {
        player.EnableDoubleJump();
        _active = true;
    }

    public override void RemoveEffect(PlayerController player)
    {
        if (_active)
        {
            player.DisableDoubleJump();
            _active = false;
        }
    }

    /* private PlayerController _playerController;
     private float _effectDuration = 10f;
     private bool _active = false;

     protected override void ApplyEffect(PlayerController player)
     {
         _playerController = player;
         _playerController.EnableDoubleJump();
         _active = true;
         StartCoroutine(EffectDuration());
     }

     protected override void RemoveEffect(PlayerController player)
     {
         if (_active)
         {
             _playerController.DisableDoubleJump();
             _active = false;
         }
     }

     private IEnumerator EffectDuration()
     {
         yield return new WaitForSeconds(_effectDuration);
         RemoveEffect(_playerController);
     }*/
}
