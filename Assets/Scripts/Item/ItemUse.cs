using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public GameObject leftGrabPositon;
    [SerializeField]
    private InstallObject installObject;
    public void Use()
    {
        if (leftGrabPositon.GetComponentInChildren<ItemPickUp>() != null)                                               // �տ� �������� �ִ��� üũ
        {
            Item.ItemType item = leftGrabPositon.transform.GetChild(1).GetComponent<ItemPickUp>().item.itemType;        //  itemType�� �Ҵ�
            if (item == Item.ItemType.CatPunch)
            {
                Transform itemObject = leftGrabPositon.transform.GetChild(1);                                           // ���� 1��° �ڽ� ������Ʈ �Ҵ�(��ġ�� �ٲ�� ����)
                itemObject.GetComponent<ItemCatPunch>().StartCatPunchCoroutine();
            }
            else if(item == Item.ItemType.SportsDrink)
            {
                Transform itemObject = leftGrabPositon.transform.GetChild(1);                                     // ���� 1��° �ڽ� ������Ʈ �Ҵ�(��ġ�� �ٲ�� ����)
                itemObject.GetComponent<ItemSportsDrink>().SpeedIncrease();
            }
            else if (item == Item.ItemType.Kitten)
            {
                if(installObject.isPreviewActivated == false)
                {
                    installObject.Installation(0);
                }
                if (installObject.clickNum > 0)
                {
                    Debug.Log(installObject.clickNum);
                    Transform itemObject = leftGrabPositon.transform.GetChild(1);
                    installObject.Build();
                    Debug.Log(installObject.clickNum);
                    Destroy(itemObject.gameObject);
                }
                installObject.clickNum++;
            }
        }
    }
}
