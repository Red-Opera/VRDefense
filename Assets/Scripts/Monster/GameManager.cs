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
    private AudioSource audioSource;
    public AudioClip[] clip;
    public SkillState[] logState;           // �볪�� ��ų�� �������� �� ����
    public SkillState[] paperState;         // ���� ��ų�� �������� �� ����
    private List<string> facialName = new List<string> { "Base1", "Base2", "Base3", "Pain1", "Pain2", "Smile", "Embarrassed" };
    private List<string> playerFacialName = new List<string> { "Base1", "Base2", "Base3", "Pain1", "Pain2", "Smile", "Embarrassed","Angry", "Wink1", "Wink2" };
    private List<string> monsterName = new List<string> { "Normal", "Tired", "Speed", "Tanker" };
    private List<int> playerEyebrowTimeList = new List<int> { 0, 0, 0, 2, 2, 0, 1, 1, 1, 1 };
    private List<int> eyebrowTimeList = new List<int> { 0, 0, 0, 2, 2, 0, 1 };
    private List<int> playerMouthTimeList = new List<int> { 0, 0, 3, 7, 8, 4, 5, 6, 5, 5 };
    private List<int> mouthTimeList = new List<int> { 0, 1, 3, 7, 8, 4, 5 };
    public List<FacialExpressionData> PlayerFacialData = new List<FacialExpressionData>();
    public List<FacialExpressionData> normalFacialData = new List<FacialExpressionData>();
    public List<FacialExpressionData> tiredFacialData = new List<FacialExpressionData>();
    public List<FacialExpressionData> speedFacialData = new List<FacialExpressionData>();
    public List<FacialExpressionData> tankerFacialData = new List<FacialExpressionData>();

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

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        for(int i = 0; i < playerFacialName.Count; i++)
        {
            float eyeNumber = 0.1f + i;
            FacialExpressionData expression = new FacialExpressionData
            {
                expressionName = playerFacialName[i],
                eyeAnimationTime = eyeNumber + i,
                eyebrowAnimationTime = playerEyebrowTimeList[i],
                mouthAnimationTime = playerMouthTimeList[i]
            };
            PlayerFacialData.Add(expression);
        }
        for (int i = 0; i < monsterName.Count; i++)
        {
            float eyeNumber = 10.1f + i * 7;
            // ǥ�� �����͸� �� ���� ����Ʈ�� �߰��մϴ�.
            for (int k = 0; k < facialName.Count; k++)
            {
                FacialExpressionData expression = new FacialExpressionData
                {
                    expressionName = facialName[k],
                    eyeAnimationTime = eyeNumber+k,
                    eyebrowAnimationTime = eyebrowTimeList[k],
                    mouthAnimationTime = mouthTimeList[k]
                };

                // �� ���Ϳ� �´� ����Ʈ�� �߰��մϴ�.
                if (i == 0)
                {
                    normalFacialData.Add(expression);
                }
                else if (i == 1)
                {
                    tiredFacialData.Add(expression);
                }
                else if (i == 2)
                {
                    speedFacialData.Add(expression);
                }
                else
                {
                    tankerFacialData.Add(expression);
                }
            }
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

    public void ClickButtonSound()
    {
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    public struct FacialExpressionData
    {
        public string expressionName;        // ǥ�� �̸� 
        public float eyeAnimationTime;       // �� �ִϸ��̼� �ð� (��)
        public float eyebrowAnimationTime;   // ���� �ִϸ��̼� �ð� (��)
        public float mouthAnimationTime;     // �� �ִϸ��̼� �ð�
    }
}
