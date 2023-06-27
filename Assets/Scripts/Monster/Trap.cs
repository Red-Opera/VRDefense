using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float trapDuration = 2f;  // ������ ���� �ð�

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster") // ���� ������Ʈ�� ������ ��쿡�� ����
        {
            MonsterMovement monster = other.GetComponent<MonsterMovement>();
            // ������ �ɸ� ���Ϳ� ���� ó�� �߰������� �ϼ��� ���� �� �����Ѵٸ� Ʈ�� �߾� ��ġ�� �̵���Ű�� �ڵ� �ֱ�
            monster.SetTrapped(trapDuration);
            Debug.Log("catch");
        }
    }
}

