using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AI;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;
    private bool isTrapped = false; // Ʈ�� �ɷȴ��� ����
    public float trapDuration;
    private bool isMovingToDestination = false; // �̵� ������ ���θ� ��Ÿ���� ����
    // ���� ã�Ƽ� �̵��� ������Ʈ
    NavMeshAgent agent;

    private void Awake()
    {
        // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(target.position);
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
}