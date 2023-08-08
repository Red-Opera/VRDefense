using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private bool isTrapped = false; // Ʈ�� �ɷȴ��� ����
    public float trapDuration;      // Ʈ�� ���� �ð�

    // ü�� ���� ����
    public MonsterScriptable monsterData;               // ���� ������ ��ũ���ͺ� ��ü
    public int currentHealth { get; private set; }      // ���� ü�� (�ܺο��� �б� ���)

    private Slider hpSlider;                            // ü�� �����̴�
    GameObject gameManager;
    GameObject uiManager;

    // ���� ī�װ����� ������ �迭�� �迭
    public GameObject[][] appearanceOptions;
    // ���� ī�װ���
    public GameObject[] hairOptions;
    public GameObject[] topOptions;
    public GameObject[] bottomOptions;
    public GameObject[] shoeOptions;
    public Vector3 clothsScale = new Vector3(11.0f, 11.0f, 9.0f);

    // ������ ���� ����
    private MonsterMove moveComponent;

    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();    // ���Ϳ��� hp �����̴��� ã��
        moveComponent = GetComponent<MonsterMove>();
        gameManager = GameObject.Find("GameManager");
        uiManager= GameObject.Find("UiManager");

        // ���Ϳ� ���� �����̴��� �����ϴ��� Ȯ��
        if (hpSlider == null)
            Debug.Assert(false, "Error (Monster Slider) : ���Ϳ� ü�� �ٰ� �������� �ʽ��ϴ�.");

        // ���Ϳ� �����Ӱ��� ������Ʈ�� �����ϴ��� Ȯ��
        if (moveComponent == null)
            Debug.Assert(false, "Error (Monster Move) : ���Ϳ� �����ӿ� ���� ������Ʈ�� �������� �ʽ��ϴ�.");
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // ���� ü�� �ִ� ü������ ����
        InitializeAppearanceOptions(); // ���� ī�װ� �迭 �ʱ�ȭ
        SetRandomAppearance(); // �����ϰ� �ٵ�, �ǻ�, ��� ���� �����Ͽ� ����
        SetRandomAppearance(); // �����ϰ� �ٵ�, �ǻ�, ��� ���� �����Ͽ� ����
    }

    void Update()
    {
        if (!isTrapped)
            moveComponent.InspectDestination();
    }

    public void SetTrapped(float duration)
    {
        if (duration < 0)
        {
            Debug.Assert(false, "Error (Unacceptable Value) : ���� ���̴� ������ �� �� �����ϴ�.");
            return;
        }

        if (!isTrapped)
        {
            isTrapped = true;
            moveComponent.Stop();
            trapDuration = duration;

            StartCoroutine(ReleaseFromTrap()); // ���� ���¸� ǥ���ϴ� �ִϸ��̼� ���� �߰��ϱ� 
        }
    }

    private IEnumerator ReleaseFromTrap()
    {
        yield return new WaitForSeconds(trapDuration); // ���� ����
        isTrapped = false;
    }

    public void TakeDamage(int damage) // ������ �޴� �ڵ�
    {
        currentHealth -= damage;    // ���� ü�¿��� ������ ��ŭ ���� �ڵ�

        // ü�� 0 ���Ͻ� �۵�
        if (currentHealth <= 0)
        {
            hpSlider.value = 0;
            Die(); 
        }

        // ���� ĳ���Ͱ� ü���� �������� ���
        else
            hpSlider.value = ((float)currentHealth / monsterData.maxHp) * 100;  // ���� ü���� �����̴��� �ݿ�
    }

    private void Die()// ���Ͱ� �׾��� �� ȣ��
    {
        //GameManager.Instance.AddCurrency(monsterData.coin); // ���� coin �� ��ŭ ��ȭ ����
        gameManager.GetComponent<GameManager>().AddCurrency(monsterData.coin);
        //UiManager.instance.UpdateCurrencyText(GameManager.Instance.currency);
        uiManager.GetComponent<UiManager>().UpdateCurrencyText(gameManager.GetComponent<GameManager>().currency);
        Destroy(gameObject); // ���� ���� ������Ʈ ����
    }

    private void InitializeAppearanceOptions()
    {
        // ���� ī�װ� �迭 ������ �ʱ�ȭ
        appearanceOptions = new GameObject[4][];
        appearanceOptions[0] = hairOptions;
        appearanceOptions[1] = topOptions;
        appearanceOptions[2] = bottomOptions;
        appearanceOptions[3] = shoeOptions;
    }

    private void SetRandomAppearance()
    {
        for (int i = 0; i < appearanceOptions.Length; i++)
        {
            // �ش� ī�װ��� �迭 ���̰� 0 �̻��� ��쿡�� ������ �ε��� ����
            if (appearanceOptions[i].Length > 0)
            {
                int randomIndex = Random.Range(0, appearanceOptions[i].Length);
                GameObject selectedAppearance = appearanceOptions[i][randomIndex];
                selectedAppearance.transform.localScale = clothsScale;
                Instantiate(selectedAppearance, transform);
            }
        }
    }

}
