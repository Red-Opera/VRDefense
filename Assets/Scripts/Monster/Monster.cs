using System.Collections;
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

    // �˹� ����
    Vector3 KnockBackPosition;

   
    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();    // ���Ϳ��� hp �����̴��� ã��
        moveComponent = GetComponent<MonsterMove>();

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
        
    }

    void Update()
    {
        if (isTrapped)
            return;

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

    private IEnumerator KnockBack(Vector3 weaponpos, float knockback)
    {
        //Lerp��� �з����� ������ ��� ����
        float flytime = 0.0f;

        while (flytime < 0.125) //0.2�� ���� �˹�
        {
            flytime += (Time.deltaTime);
            KnockBackPosition = transform.position + ((transform.position - weaponpos) * knockback);    //���� ��ġ - ���� ��ġ�� �з����� ������ ���ϰ� ���� �з��� ��ġ ����
            transform.position = Vector3.Lerp(transform.position, KnockBackPosition, flytime/ 0.125f);     //0.125�ʸ� �������� ����

            yield return null;
        }
        yield return null;
    }

    public void TakeDamage(int damage,Vector3 weaponpos,float knockback) // ������ �޴� �ڵ�
    {
        currentHealth -= damage;    // ���� ü�¿��� ������ ��ŭ ���� �ڵ�
        StartCoroutine(KnockBack(weaponpos, knockback));    //�˹� �ڷ�ƾ
      
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

    public void Die()// ���Ͱ� �׾��� �� ȣ��
    {
        ItemDrop();
        GameManager.gameManager.AddCurrency(monsterData.coin); // ���� coin �� ��ŭ ��ȭ ����
        
        UiManager.uiManager.UpdateCurrencyText(GameManager.gameManager.currency);
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

    private void SetRandomAppearance() // ���� �ǻ� ���� �ڵ�
    {
        for (int i = 0; i < appearanceOptions.Length; i++)
        {
            // �ش� ī�װ��� �迭 ���̰� 0 �̻��� ��쿡�� ������ �ε��� ����
            if (appearanceOptions[i].Length > 0)
            {
                int randomIndex = Random.Range(0, appearanceOptions[i].Length);
                GameObject selectedAppearancePrefab = appearanceOptions[i][randomIndex];
                GameObject selectedAppearance = Instantiate(selectedAppearancePrefab, transform.position, transform.rotation, transform);
                selectedAppearance.transform.localScale = clothsScale;
                Animator appearanceAnim = selectedAppearance.GetComponent<Animator>();
                if (appearanceAnim == null)
                {
                    appearanceAnim = selectedAppearance.AddComponent<Animator>();
                }
                appearanceAnim.runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;
            }
        }
    }

    public void ItemDrop()
    {
        Choose(new float[2] { 10f, 90f });
        
        float Choose(float[] probs)
        {
            
            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    switch(i)
                    {
                        case 0:
                            int rand = Random.Range(0, monsterData.dropItem.Length);
                            Instantiate(monsterData.dropItem[rand], transform.position, Quaternion.identity);
                            break;
                        case 1:
                            break;
                    }
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }

    }
}