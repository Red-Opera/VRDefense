using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FORGE : MonoBehaviour
{
    GameObject Object;

    private void Start()
    {
        Object = GameObject.Find("RightControllerGrabposition");
    }
    private void OnTriggerEnter(Collider other)
    {
        //�� ������� Forge�� ��´ٸ� 1�� ��ȭ�ǰ� �����
        if(other.CompareTag("ShopBat"))
        {
            UpgradeBat();
        }
        if (other.CompareTag("ShopRacket"))
        {
            UpgradeRacket();
        }
        if (other.CompareTag("ShopWrench"))
        {
            UpgradeWrench();
        }
    }
    private void UpgradeBat()
    {
        if (Object.GetComponent<Grab>().BatLevel >= 3)
        {
            Debug.Log("Ǯ���Դϴ�");
        }
        else
        {
            Destroy(gameObject);
            Object.GetComponent<Grab>().BatLevel += 1;
        }
    }
    private void UpgradeRacket()
    {
        if (Object.GetComponent<Grab>().RacketLevel >= 3)
        {
            Debug.Log("Ǯ���Դϴ�");
        }
        else
        {
            Destroy(gameObject);
            Object.GetComponent<Grab>().RacketLevel += 1;
        }
    }
    private void UpgradeWrench()
    {
        if (Object.GetComponent<Grab>().WrenchLevel >= 3)
        {
            Debug.Log("Ǯ���Դϴ�");
        }
        else
        {
            Destroy(gameObject);
            Object.GetComponent<Grab>().WrenchLevel += 1;
        }
    }

}
