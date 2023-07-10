using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabWeapon : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform GrabPositon;
    private GameObject Weapon;
    void FixedUpdate()
    {
        if(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))       //��ư�� ������ ���� ��
        {
            isGrabbing = true;
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))        //��ư�� ������ ��
        {
            isGrabbing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Weapon = other.gameObject;

        if(other.CompareTag("Weapon"))
        {
            if(isGrabbing)      //���� ��´ٸ�
            {
                //������ Weapon�� ��ġ ���� �տ� ����
                Weapon.transform.position = GrabPositon.transform.position;
                Weapon.transform.rotation = GrabPositon.transform.rotation;

                //Weapon�� ���� ���� ��ü�� ����
                Weapon.transform.SetParent(GrabPositon);

                //�տ� �����ϱ� ���� iskinematic Ȱ��
                Weapon.GetComponent<Rigidbody>().isKinematic = true;
            }

            else if(!isGrabbing)        //���´ٸ�
            {
                //Weapon�� ���� ���� ��ü���� ����
                Weapon.transform.SetParent(null);

                //�տ� ���� ���� iskinematic ��Ȱ��
                Weapon.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

}
