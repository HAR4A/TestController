using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    public static BonusManager Instance { get; private set; }

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
        StartCoroutine(EffectDuration(bonus, player));
    }

    private IEnumerator EffectDuration(GameObject bonus, PlayerController player)
    {
        Bonus bonusScript = bonus.GetComponent<Bonus>();
        if (bonusScript != null)
        {
            bonusScript.ApplyEffect(player);
            bonus.SetActive(false);
            Debug.Log($"{bonus.name} deactivated");

            // Запускаем параллельную корутину для восстановления бонуса через 5 секунд
            StartCoroutine(ReactivateBonus(bonus));

            yield return new WaitForSeconds(10f); // Длительность эффекта 10 секунд
            bonusScript.RemoveEffect(player);
            Debug.Log($"{bonus.name} effect removed");
        }
    }

    private IEnumerator ReactivateBonus(GameObject bonus)
    {
        yield return new WaitForSeconds(5f); // Время до повторного появления 5 секунд
        Debug.Log($"{bonus.name} reactivated");
        bonus.SetActive(true); // Активируем объект после прошествия 5 секунд
    }
}
