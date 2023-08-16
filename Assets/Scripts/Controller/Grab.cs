using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grab : MonoBehaviour
{
    private bool isGrabbing = false;
    public Transform leftGrabPositon;
    public Transform rightGrabPosition;
    public GameObject itemData;
    public int BatLevel = 1;
    public int RacketLevel = 1;
    public int WrenchLevel = 1;
    public ItemSlot itemSlot;
    private GameObject Grabbable;
    private LineRenderer lineRenderer;
    private RaycastHit hit;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))       //��ư�� ������ ��
        {
            isGrabbing = true;
        }
        else if(OVRInput.GetUp(OVRInput.RawButton.A))        //A ��ư�� ������ ���´�
        {
            isGrabbing = false;
        }

        if (!isGrabbing)        //���� ���´ٸ� ��� ���⸦ ��Ȱ�� �Ѵ�.
        {
            ChangeWeapon(false, false, false);
        }

        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) || OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            ItemGrab();
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            lineRenderer.enabled = false;
        }

        if(OVRInput.Get(OVRInput.RawButton.B))
        {
            itemData.GetComponent<ItemUse>().Use();         // ������ ���
        }
    }

    private void OnTriggerEnter(Collider other)     //Enter�̿��� ��ư���� ���·� ������Ʈ�� ����������
    {
        Grabbable = other.gameObject;

        if (other.CompareTag("Grabbable"))
        {
            if (isGrabbing)      //���� ��´ٸ�
            {
                //������ Grabbable�� ��ġ ���� �տ� ����
                Grabbable.transform.position = rightGrabPosition.transform.position;
                Grabbable.transform.rotation = rightGrabPosition.transform.rotation;

                //Grabbable�� ���� ���� ��ü�� ����
                Grabbable.transform.SetParent(rightGrabPosition);

                //�տ� �����ϱ� ���� iskinematic Ȱ��
                Grabbable.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Grabbable = other.gameObject;

        if(other.CompareTag("Grabbable"))
        {
            if(!isGrabbing)        //���´ٸ�
            {
                //Grabbable ���� ���� ��ü���� ����
                Grabbable.transform.SetParent(null);

                //�տ� ���� ���� iskinematic ��Ȱ��
                Grabbable.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if(other.CompareTag("ShopBat"))
        {
            if(isGrabbing)
            {
                ChangeWeapon(true, false, false);       //bat�� Ȱ��ȭ
            }
        }
        if (other.CompareTag("ShopRacket"))
        {
            if (isGrabbing)
            {
                ChangeWeapon(false, true, false);       //Racket�� Ȱ��ȭ
            }
        }
        if (other.CompareTag("ShopWrench"))
        {
            if (isGrabbing)
            {
                ChangeWeapon(false, false, true);       //wrench�� Ȱ��ȭ
            }
        }
    }

    private void ChangeWeapon(bool bat, bool racket, bool wrench)
    {
        //1���� �� 1�� ���� Ȱ��
        if (BatLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + BatLevel).gameObject.SetActive(bat);
        }
        //1�� �ʰ��� �� ���ܰ蹫�� ��Ȱ�� �� ���� ����Ȱ��
        else if (1 < BatLevel && BatLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + BatLevel).gameObject.SetActive(bat);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Bat" + (BatLevel - 1)).gameObject.SetActive(false);
        }
        else if(BatLevel > 3)
        {
            Debug.Log("err");
        }

        //1���� �� 1�� ���� Ȱ��
        if (RacketLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + RacketLevel).gameObject.SetActive(racket);
        }
        //1�� �ʰ��� �� ���ܰ蹫�� ��Ȱ�� �� ���� ����Ȱ��
        else if (1 < RacketLevel && RacketLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + RacketLevel).gameObject.SetActive(racket);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Racket" + (RacketLevel - 1)).gameObject.SetActive(false);
        }
        else if (RacketLevel > 3)
        {
            Debug.Log("err");
        }
        //1���� �� 1�� ���� Ȱ��hzl

        if (WrenchLevel == 1)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + WrenchLevel).gameObject.SetActive(wrench);
        }
        //1�� �ʰ��� �� ���ܰ蹫�� ��Ȱ�� �� ���� ����Ȱ��
        else if (1 < WrenchLevel && WrenchLevel <= 3)
        {
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + WrenchLevel).gameObject.SetActive(wrench);
            rightGrabPosition.Find("WP_Bundle").transform.Find("Wrench" + (WrenchLevel - 1)).gameObject.SetActive(false);
        }
        else if (WrenchLevel > 3)
        {
            Debug.Log("err");
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

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.tag == "Item")
            {
                if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))        // ��ư ������
                {
                    hit.transform.GetComponent<HoverItem2>().itemRotation = false;                          // ������ ȸ�� ����
                    hit.transform.position = leftGrabPositon.transform.position;    // ������ �� ��ġ��
                    hit.transform.SetParent(leftGrabPositon);
                    hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                    if (this.name == "RightControllerGrabposition")                                         // this�� ������ ��Ʈ�ѷ��� �Ǹ� PickUp�Լ��� ������, ���� �� ������ǹǷ� this üũ
                    {
                        return;
                    }
                    else
                    {
                        PickUp();
                    }
                }
            }
        }
    }

    private void PickUp()
    {
        if(hit.collider.tag == "Item")
        {
            Debug.Log(hit.transform.GetComponent<ItemPickUp>().item.itemName + "ȹ��");
            itemSlot.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
            Destroy(hit.transform.gameObject, 1f);                  // ����
        }
    }
}
