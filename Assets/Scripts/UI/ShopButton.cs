using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int price; // ��ǰ ����

    private void BuyItem()
    {
        if (GameManager.Instance.SpendCurrency(price)) // ���� �Ŵ��� �ڵ� �� ������ true �� �� �Ҹ� ������ false
        {
            // ��ǰ ���ſ� �������� �� ��� �ۼ��ϱ�
            
        }
        else
        {
            // ��ǰ ���ſ� ������ ��� �޽����� �� ������ �˷��ִ� ǥ�� �����ֱ�
            
        }
    }
}
