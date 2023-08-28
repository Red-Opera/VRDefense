using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLullaby : MonoBehaviour
{
    [SerializeField]
    private float objectDuration = 20.0f;                           //������ ��ȿ�ð�
    [SerializeField]
    private float lullabyDuration = 10.0f;                          //���� �̵�����ð�(������, ����)
    
    private bool isObtained = false;                                //slot�� �������� ����
    private bool isInstall = false;                                //�������� �ٴڿ� ��ġ�ƴ����� ����
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

        Transform child = transform.GetChild(0);                   
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
        if (isInstall)                                      //�������� ��ġ�� ������ ��
        {
            transform.position = pos;                       
            if (!isWaitingToDestroy)                        //���� ��� ���� �ƴϸ�
            {
                itemDetect.SetLullabyDuration(lullabyDuration);     //Monster�̵�����ð� �ѱ�
                itemDetect.SetIsActive(true);                       //Monster���� Ȱ��ȭ
                StartCoroutine(DestroyLullaby());           //������ ���� Ÿ�̸� ����
                isWaitingToDestroy = true;                  //���� ��� ������ ����
                rigid.isKinematic = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)      //�������� ������ ��ġ�Ѵٰ� �������� ��
    {      
        if (collision.gameObject.CompareTag("Floor"))       //�ٴڰ� �浹�ϰ�
        {      
            if (!isInstall)                                 //�������� ��ġ�� ���°� �ƴ� ��
            {       
                pos = transform.position;
                isInstall = true;                           //��ġ������ ���� ����
            }
        }
    }

    private IEnumerator DestroyLullaby()
    {
        yield return new WaitForSeconds(objectDuration);
        Destroy(gameObject);                                //�����ð� ���� ������ ����
    }

    public void SetIsObtain(bool value)
    {
        isObtained = value;
    }
}
