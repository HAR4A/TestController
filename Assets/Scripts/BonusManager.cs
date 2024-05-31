using System.Collections;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance { get; private set; }       //store a single instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActivateBonus(GameObject bonus, PlayerController player)
    {
        Bonus bonusScript = bonus.GetComponent<Bonus>();
        if (bonusScript != null)
        {         
            bonusScript.ApplyEffect(player); //Applies an effect to the player
            bonus.SetActive(false);
          //Debug.Log($"{bonus.name} deactivated");
            StartCoroutine(ReactivateBonus(bonus));
        }
    }

    private IEnumerator ReactivateBonus(GameObject bonus)
    {
        yield return new WaitForSeconds(5f);
      //Debug.Log($"{bonus.name} reactivated");
        bonus.SetActive(true);
    }
}
