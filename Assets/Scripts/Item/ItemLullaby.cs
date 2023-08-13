using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLullaby : MonoBehaviour
{
    
    public float validTime = 10.0f;                               //������ ��ȿ�ð�
 
    private bool isObtained = true;                               //�������� ����ڿ��� ȹ��� ���� �ִ��� ����
                                                                   //������ ȹ���� �����ϴ� �ڵ忡�� true�� ��������� ��
    private bool isInstall = false;                                //������ ��ġ ����
    private bool isWaitingToDestroy = false;                        //�������� ���� ��� �������� ����
    
    private Rigidbody rigid;

    ItemDetectMonster itemDetect = null;
    Vector3 pos;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.Assert(false, "Error (RigidBody is Null) : �ش� ��ü�� RigidBody�� �������� �ʽ��ϴ�.");
            return;
        }

        Transform child = transform.GetChild(0);                    //ù��° �ڽ� ��ü�� �ҷ���
        if (child == null) {
            Debug.Assert(false, "Error (There is no child) : �ش� ��ü�� child�� �������� �ʽ��ϴ�.");
            return;
        }

        itemDetect = child.GetComponent<ItemDetectMonster>();
        if (itemDetect == null)
        {
            Debug.Assert(false, "Error (There is no Script) : �ش� ��ü�� �ش� ��ũ��Ʈ�� �������� �ʽ��ϴ�.");
            return;
        }
    }

    private void Start() {
        pos = transform.position;
    }

    private void Update() 
    {
        if (isInstall) 
        {
            transform.position = pos;
            if (!isWaitingToDestroy)
            {
                itemDetect.SetIsActive(true);
                Invoke("DestroyObject", validTime); //���� ��� ������ ���� ��ȭ
                isWaitingToDestroy = true;
                rigid.isKinematic = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Floor"))
        {   
            if (isObtained && !isInstall)
            {   
                pos = transform.position;
                isInstall = true;
            }
        }
    }

    private void DestroyObject() 
    {
        Destroy(gameObject);
    }

    public void SetIsObtain(bool value)
    {
        isObtained = value;
    }
}
