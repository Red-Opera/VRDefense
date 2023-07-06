using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public int damageAmount; // �� ����� �� ���� ��������
    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();
        if (other.CompareTag("Monster"))
        {
            ApplyDamageToGoal(monster);
            Destroy(other.gameObject); // ������ ��� �ش� ��ü�� �ı�
        }
    }

    private void ApplyDamageToGoal(Monster monster) // �� ���� ������ ������ ������ �����ͼ� ���� ü�¿� �������� ����
    {
        damageAmount = monster.monsterData.damage; // ������ ������ ���� ��������
        GameManager.Instance.DecreaseHealth(damageAmount); // ü�� ����
    }
}
