using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public GameObject leftGrabPositon;
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
        }
    }
}
