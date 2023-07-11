using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrabWeapon : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform GrabPositon;
    private GameObject Weapon;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

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

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            ItemGrab();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            lineRenderer.enabled = false;
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

    private void ItemGrab()
    {
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        //Debug.DrawRay(GrabPositon.position, GrabPositon.forward * 10, Color.blue);

        // ���� ������
        lineRenderer.SetPosition(0, transform.position);
        // ���� ������
        lineRenderer.SetPosition(1, transform.position + (transform.forward * 10));
        lineRenderer.enabled = true;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.tag == "Item")
            {
                if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))        // ��ư ������
                {
                    hit.transform.position = GrabPositon.transform.position;
                    hit.transform.SetParent(GrabPositon);
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    Destroy(hit.transform.gameObject, 1f);                  // ����
                }
            }
        }
    }
}
