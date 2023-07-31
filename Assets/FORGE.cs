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
        Destroy(gameObject);
        Object.GetComponent<Grab>().BatLevel += 1;
    }
    private void UpgradeRacket()
    {
        Destroy(gameObject);
        Object.GetComponent<Grab>().RacketLevel += 1;
    }
    private void UpgradeWrench()
    {
        Destroy(gameObject);
        Object.GetComponent<Grab>().WrenchLevel += 1;
    }

}
