using System.Collections;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    public abstract void ApplyEffect(PlayerController player);
    public abstract void RemoveEffect(PlayerController player);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                BonusManager.Instance.ActivateBonus(gameObject, player);
            }
        }
    }

}
