using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int maxHealth = 45; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public int currency; // ��ȭ
    public int weaponLevel = 1;
    public string weaponName;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Button")
            {
                print("It's working");
            }
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;  // ���� �� ��ü ü������ �ʱ�ȭ
        currency = 0;               // ���� �� ��ȭ�� 0���� �ʱ�ȭ

        // UiManager�� �����ϴ��� Ȯ�� ��
        if (UiManager.uiManager == null)
        {
            Debug.Assert(false, "Error (UiManager is Null) : UiManager�� ã�� �� �����ϴ�.");
            return;
        }

        UiManager.uiManager.UpdateHealthText(currentHealth, maxHealth);
        UiManager.uiManager.UpdateCurrencyText(currency);
    }

    public void DecreaseHealth(int amount) // ü�� ���� �޼���
    {
        currentHealth -= amount;
        if (currentHealth <= 0) // ü�� 0 �Ǹ� ���� ����
        {
            Debug.Log("Game Over");
        }
    }


    public void AddCurrency(int amount)   // ��ȭ �߰�
    {
        currency += amount;
    }


    public bool SpendCurrency(int amount)   // ��ȭ ����
    {
        if (currency >= amount) // �� �ִ� ���
        {
            currency -= amount;
            return true;
        }
        else // �� ������ ���
        {
            return false;
        }
    }
}
