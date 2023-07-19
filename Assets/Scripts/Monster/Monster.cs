using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target; // ��ǥ ����
    private bool isTrapped = false; // Ʈ�� �ɷȴ��� ����
    public float trapDuration; // Ʈ�� ���� �ð�
    private bool isMovingToDestination = false; // �̵� ������ ���θ� ��Ÿ���� ����
    NavMeshAgent agent; // ���� ã�Ƽ� �̵��� ������Ʈ

    public MonsterScriptable monsterData; // ���� ������ ��ũ���ͺ� ��ü
    private int currentHealth; // ���� ü��

    // ���� ī�װ����� ������ �迭�� �迭
    public GameObject[][] appearanceOptions;
    // ���� ī�װ���
    public GameObject[] hairOptions;
    public GameObject[] topOptions;
    public GameObject[] bottomOptions;
    public GameObject[] shoeOptions;
    public Vector3 clothsScale = new Vector3(11.0f, 11.0f, 9.0f);

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // ���� ü�� �ִ� ü������ ����
        agent.SetDestination(target.position); // ������ ����
        agent.speed = monsterData.moveSpeed; // ���� �̵� �ӵ� �����Ϳ��� �޾ƿͼ� ����
        InitializeAppearanceOptions(); // ���� ī�װ� �迭 �ʱ�ȭ
        SetRandomAppearance(); // �����ϰ� �ٵ�, �ǻ�, ��� ���� �����Ͽ� ����
    }

    void Update()
    {
        if (!isTrapped)
        {
            if (!isMovingToDestination) // Ʈ���� �ɸ��� ���� �����ε� �������� �̵������� ������ ������ ��������
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                isMovingToDestination = true; // �������� �̵� ������ �ٲ� agent.SetDestination(target.position); �� �Լ� �ѹ��� �����ϰ� ��
            }
        }
    }

    public void SetTrapped(float duration)
    {
        if (!isTrapped)
        {
            isTrapped = true;
            isMovingToDestination = false;
            agent.isStopped = true; // agent �̵� ���߱�
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
        currentHealth -= damage; // ���� ü�¿��� ������ ��ŭ ���� �ڵ�
        if (currentHealth <= 0) // ü�� 0 ���Ͻ� �۵�
        {
            Die(); 
        }
    }

    private void Die()// ���Ͱ� �׾��� �� ȣ��
    {
        GameManager.Instance.AddCurrency(monsterData.coin); // ���� coin �� ��ŭ ��ȭ ����
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
