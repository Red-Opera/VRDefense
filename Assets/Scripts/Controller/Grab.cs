using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grab : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform leftGrabPositon;
    public Transform rightGrabPosition;
    private GameObject weapon;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))       //��ư�� ������ ���� ��
        {
            isGrabbing = true;
        }
        else if(OVRInput.GetUp(OVRInput.RawButton.A))        //��ư�� ������ ��
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

    private void OnTriggerEnter(Collider other)
    {
        weapon = other.gameObject;

        if (other.CompareTag("Weapon"))
        {
            if (isGrabbing)      //���� ��´ٸ�
            {
                //������ Weapon�� ��ġ ���� �տ� ����
                weapon.transform.position = rightGrabPosition.transform.position;
                weapon.transform.rotation = rightGrabPosition.transform.rotation;

                //Weapon�� ���� ���� ��ü�� ����
                weapon.transform.SetParent(rightGrabPosition);

                //�տ� �����ϱ� ���� iskinematic Ȱ��
                weapon.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        weapon = other.gameObject;

        if(other.CompareTag("Weapon"))
        {
            if(!isGrabbing)        //���´ٸ�
            {
                //Weapon�� ���� ���� ��ü���� ����
                weapon.transform.SetParent(null);

                //�տ� ���� ���� iskinematic ��Ȱ��
                weapon.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void ItemGrab()
    {
        Debug.DrawRay(leftGrabPositon.position, leftGrabPositon.forward * 10, Color.red);
        //Debug.DrawRay(GrabPositon.position, GrabPositon.forward * 10, Color.blue);

        // ���� ������
        lineRenderer.SetPosition(0, leftGrabPositon.position);
        // ���� ������
        lineRenderer.SetPosition(1, leftGrabPositon.position + (leftGrabPositon.forward * 10));
        lineRenderer.enabled = true;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.tag == "Item")
            {
                if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))        // ��ư ������
                {
                    hit.transform.position = leftGrabPositon.transform.position;
                    hit.transform.SetParent(leftGrabPositon);
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    Destroy(hit.transform.gameObject, 1f);                  // ����
                }
            }
        }
    }
}
