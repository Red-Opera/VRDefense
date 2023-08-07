using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currencyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ü�� ������Ʈ
    public void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + " / " + maxHealth;
    }

    // ��ȭ ������Ʈ
    public void UpdateCurrencyText(int currency)
    {
        currencyText.text = currency.ToString();
    }
}
