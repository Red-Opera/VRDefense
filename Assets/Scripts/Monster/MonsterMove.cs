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
    public List<Transform> target;             // ���Ͱ� �������� �̵��ؾ� �� ��ġ ����
    public int tarPosIndex = 0;                // ���Ͱ� ������ �̵��ؾ� �� ��ǥ ������ �ε���
    private Rigidbody rigid;
    private Collider collid;

    private float originalSpeed;
    private float currentSpeed;
    private bool isSlowingDown = false;
    private bool isStopByLullaby = false;
    private float lullabyDuration;

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
        originalSpeed = monsterData.moveSpeed;      //�ʱ� �̵��ӵ� ����
        currentSpeed = monsterData.moveSpeed;
    }

    void Update()
    {
        if (isSlowingDown) 
        {
            SlowingDown();
        }
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

    public void Move()
    {
        isStop = true;
        agent.isStopped = false;
        collid.isTrigger = false;
        agent.SetDestination(target[tarPosIndex].position);
    }

    public void SetIsSlowingDown(float duration)
    {
        isSlowingDown = true;           
        lullabyDuration = duration;
    }

    public void SlowingDown()
    {
        if (currentSpeed >= 0.001f)
        {
            currentSpeed -= originalSpeed / lullabyDuration * Time.deltaTime;   //duration���� �ӵ��� ���� 0���� �پ��
            agent.speed = currentSpeed;
            Debug.Log("SlowingDown" + agent.speed);
        }
        else                                                                    //�ӵ��� 0�� ��������� 
        {
            Stop();                                                             //����
            agent.speed = originalSpeed;                                        //�ӵ� ���󺹱�
            isSlowingDown = false;
        }
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
    public bool GetIsStop() 
    {
        return isStop;
    }

    public void SetMoveTimer(float timer) 
    {
        Invoke("Move", timer);
    }
}