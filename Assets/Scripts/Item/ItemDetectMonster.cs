using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetectMonster : MonoBehaviour
{
    //trigger Collider�� Item�� �ڽ� ��ü�� �����ؼ� ����

    private bool isActive = false;                      //������ Ȱ��ȭ ����, �θ� ��ü ��ũ��Ʈ���� ���� ���� ������
    private string parentTag = null;                    //�θ� ��ü�� �±׸� �����ϴ� ����

    private void Awake()
    {
        if (transform.parent == null) {
            Debug.Assert(false, "Error (There is no Parent) : �ش� ��ü�� parent�� �������� �ʽ��ϴ�.");
            return;
        }
        parentTag = transform.parent.tag;               //�θ� ��ü�� �±׸� ����
    }

    private void OnTriggerStay(Collider other) 
    {
        if (isActive)                                   //�������� Ȱ��ȭ�� ���¿���
        {
            if (other.gameObject.CompareTag("Monster")) //�浹�� object�� Monster�̰�
            {  
                if (parentTag == "ItemLullaby")         //���尡 �������̸�
                {
                    MonsterMove monsterMove = other.GetComponent<MonsterMove>();
                    if (monsterMove != null)
                    {
                        monsterMove.Stop();             //Monster�� monsterMove ��ũ��Ʈ�� �ҷ��� Monster�� �������� ���ߴ� Stop �Լ��� ȣ����
                    }
                }
            }
        }
    }

    public void SetIsActive(bool value)                 //isActive������ �ٸ� ��ũ��Ʈ���� ������ �� �ְ� �ϴ� �Լ�
    {
        isActive = value;
    }
}
