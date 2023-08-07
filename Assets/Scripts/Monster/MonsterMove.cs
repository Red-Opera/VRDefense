using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private MonsterScriptable monsterData;

    private bool isMovingToDestination = false; // �̵� ������ ���θ� ��Ÿ���� ����
    private NavMeshAgent agent;                 // ���� ã�Ƽ� �̵��� ������Ʈ

    public GameObject targetSet;                // ���Ͱ� �������� �̵��ؾ� �� ��ġ�� ��� ����
    private List<Transform> target;             // ���Ͱ� �������� �̵��ؾ� �� ��ġ ����
    private int tarPosIndex = 0;                // ���Ͱ� ������ �̵��ؾ� �� ��ǥ ������ �ε���

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        target = new List<Transform>();

        int childNum = targetSet.transform.childCount;

        for (int i = 0; i < childNum; i++)
            target.Add(targetSet.transform.GetChild(i).transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(target[0].position);   // ������ ����
        agent.speed = monsterData.moveSpeed;        // ���� �̵� �ӵ� �����Ϳ��� �޾ƿͼ� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���� ���Ͱ� ������ ���� �̵��ϴ��� �˻��ϴ� �޼ҵ�
    public void InspectDestination()
    {
        if (!isMovingToDestination) // Ʈ���� �ɸ��� ���� �����ε� �������� �̵������� ������ ������ ��������
        {
            agent.isStopped = false;
            agent.SetDestination(target[tarPosIndex].position);
            isMovingToDestination = true; // �������� �̵� ������ �ٲ� agent.SetDestination(target.position); �� �Լ� �ѹ��� �����ϰ� ��
        }
    }

    // ���͸� ���߰� �ϴ� �޼ҵ�
    public void Stop()
    {
        isMovingToDestination = false;
        agent.isStopped = true;         // agent �̵� ���߱�
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Monster")
        {
            tarPosIndex++;
            agent.SetDestination(target[tarPosIndex].position);
        }
    }
}