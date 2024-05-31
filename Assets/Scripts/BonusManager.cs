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

            // ��������� ������������ �������� ��� �������������� ������ ����� 5 ������
            StartCoroutine(ReactivateBonus(bonus));

            yield return new WaitForSeconds(10f); // ������������ ������� 10 ������
            bonusScript.RemoveEffect(player);
            Debug.Log($"{bonus.name} effect removed");
        }
    }

    private IEnumerator ReactivateBonus(GameObject bonus)
    {
        yield return new WaitForSeconds(5f); // ����� �� ���������� ��������� 5 ������
        Debug.Log($"{bonus.name} reactivated");
        bonus.SetActive(true); // ���������� ������ ����� ���������� 5 ������
    }
}
