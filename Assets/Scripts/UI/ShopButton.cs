using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int price; // ��ǰ ����

    public void BuyItem()
    {
        if (GameManager.gameManager.SpendCurrency(price)) // ���� �Ŵ��� �ڵ� �� ������ true �� �� �Ҹ� ������ false
        {
            // ��ǰ ���ſ� �������� �� ��� �ۼ��ϱ�
            Debug.Log("���ż���");
        }
        else
        {
            // ��ǰ ���ſ� ������ ��� �޽����� �� ������ �˷��ִ� ǥ�� �����ֱ�
            Debug.Log("���Ž���");
        }
    }
}
