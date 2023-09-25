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
    public int score;
    public string studentId;
    private int currentMonsterCount = 0;
    private bool spawnFinished = false;
    private int currentWave = 0;

    public SkillState[] logState;           // �볪�� ��ų�� �������� �� ����
    public SkillState[] paperState;         // ���� ��ų�� �������� �� ����

    public Score rankingObject;
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

        DontDestroyOnLoad(gameObject);
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
        currency = 1000;               // ���� �� ��ȭ�� 0���� �ʱ�ȭ

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
            rankingObject.AddHighScoreEntry(score, studentId);
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

    public void AddMonster()
    {
        currentMonsterCount++;
    }
    public void DieMonster()
    {
        currentMonsterCount--;
        if (spawnFinished == true && currentMonsterCount == 0)
        {
            StageClear();
        }
    }
    public void FinishSpawn()
    {
        spawnFinished = true;
    }

    public void StageClear()
    {
        // �������� Ŭ���� ���� �� �۵��� �͵� �ֱ� ��ų ��ȭ, �̵� ��Ż ����, �� �� ����Ʈ ��
        Debug.Log("StageClear");
        currentWave++;
        spawnFinished = false;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
