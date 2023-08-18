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
                    //Transform itemObject = leftGrabPositon.transform.GetChild(1);
                    installObject.Installation(0);
                    Debug.Log(installObject.clickNum);
                }
                //else if (installObject.clickNum > 1)
                //{
                //    Transform itemObject = leftGrabPositon.transform.GetChild(1);
                //    installObject.Build();
                //    Debug.Log(installObject.clickNum);
                //    Destroy(itemObject.gameObject);
                //}
            }
        }
    }
}
