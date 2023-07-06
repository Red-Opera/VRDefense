using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    private bool isTrapped = false; // Ʈ�� �ɷȴ��� ����
    public float trapDuration; // Ʈ�� ���� �ð�
    private bool isMovingToDestination = false; // �̵� ������ ���θ� ��Ÿ���� ����
    NavMeshAgent agent; // ���� ã�Ƽ� �̵��� ������Ʈ

    public MonsterScriptable monsterData; // ���� ������ ��ũ���ͺ� ��ü
    private int currentHealth; // ���� ü��
    private void Awake()
    {
       
        agent = GetComponent<NavMeshAgent>(); // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
    }
    private void Start()
    {
        currentHealth = monsterData.maxHp; // ���� ü�� �ִ� ü������ ����
        agent.SetDestination(target.position); // ������ ����
        agent.speed = monsterData.moveSpeed; // ���� �̵� �ӵ� �����Ϳ��� �޾ƿͼ� ����
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
}
