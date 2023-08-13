using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptable monsterData;

    private bool isStop = false; // �̵� ������ ���θ� ��Ÿ���� ����
    private NavMeshAgent agent;                 // ���� ã�Ƽ� �̵��� ������Ʈ

    public GameObject targetSet;                // ���Ͱ� �������� �̵��ؾ� �� ��ġ�� ��� ����
    private List<Transform> target;             // ���Ͱ� �������� �̵��ؾ� �� ��ġ ����
    private int tarPosIndex = 0;                // ���Ͱ� ������ �̵��ؾ� �� ��ǥ ������ �ε���

    private Rigidbody rigid;
    private Collider collid;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        target = new List<Transform>();
        rigid = GetComponent<Rigidbody>();
        collid = GetComponent<Collider>();
        if (agent == null)
        {
            Debug.Assert(false, "Error (NavMeshAgent is Null) : �ش� ��ü�� NavMeshAgent�� �������� �ʽ��ϴ�.");
            return;
        }

        if (rigid == null)
        {
            Debug.Assert(false, "Error (RigidBody is Null) : �ش� ��ü�� RigidBody�� �������� �ʽ��ϴ�.");
            return;
        }

        if (collid == null)
        {
            Debug.Assert(false, "Error (NavMeshAgent is Null) : �ش� ��ü�� Collider�� �������� �ʽ��ϴ�.");
            return;
        }
        // ��ǥ������ ����
        int childNum = targetSet.transform.childCount;

        // ��ǥ������ ������
        for (int i = 0; i < childNum; i++)
            target.Add(targetSet.transform.GetChild(i).transform);
    }

    void Start()
    {
        agent.SetDestination(target[0].position);   // ������ ����
        agent.speed = monsterData.moveSpeed;        // ���� �̵� �ӵ� �����Ϳ��� �޾ƿͼ� ����
    }

    void Update()
    {
        
    }

    // ���� ���Ͱ� ������ ���� �̵��ϴ��� �˻��ϴ� �޼ҵ�
    public void InspectDestination()
    {
        // �̲��� ����
        rigid.velocity = Vector3.zero;

        // ���� ���Ͱ� �̵����� �ʴ´ٸ� ���� ��ũ��Ʈ�� �������� ����
        if (!isStop)
            return;

        // ���Ͱ� �̵������� ���� �� ���� ��ġ�� �̵��ϰ� ����
        agent.isStopped = false;
        agent.SetDestination(target[tarPosIndex].position);
        isStop = true; // �������� �̵� ������ �ٲ� agent.SetDestination(target.position); �� �Լ� �ѹ��� �����ϰ� ��
    }

    // ���͸� ���߰� �ϴ� �޼ҵ�
    public void Stop()
    {
        isStop = false;
        agent.isStopped = true;             // agent �̵� ���߱�
        rigid.velocity = Vector3.zero;
        collid.isTrigger = true;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MoveTarget")
        {
            string name = collider.gameObject.name;

            if (name == "Goal")
            {
                gameObject.SetActive(false);
                return;
            }

            tarPosIndex = Convert.ToInt32(name.Substring(name.Length - 1, 1));
            agent.SetDestination(target[tarPosIndex].position);
        }
    }
}