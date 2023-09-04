using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public GameObject itemDisplay;                          // �������� ������ ���

    private List<KeyValuePair<string, GameObject>> items;   // ������ ����� ����Ʈ�� ����
    private static string nowOn = "";                       // ���� ���� ���׿� ǥ�õǴ� ������ �̸�

    public string content;                                  // �� ��ũ��Ʈ�� ���� ��ü�� ����
    public int cost;                                        // ���� ���
    public Text displayContent;                             // ������ ǥ���� Text
    public Text displayCost;                                // ���� ����� ǥ���� Text

    public void Awake()
    {
        items = new List<KeyValuePair<string, GameObject>>();

        Debug.Assert(itemDisplay != null, "Error (GameObject is Null) : ������ ��ġ�� �������� �ʽ��ϴ�.");

        // �������� ����� ������ �̸��� ������Ʈ�� ����Ǵ� ����Ʈ�� ����
        for (int i = 0; i < itemDisplay.transform.childCount; i++)
        {
            GameObject inObject = itemDisplay.transform.GetChild(i).gameObject;

            items.Add(new KeyValuePair<string, GameObject>(inObject.name, inObject));
        }

        // ù��° �������� ���λ��׿� ǥ��
        nowOn = items[0].Key;

        if (displayContent.text.Equals("������ �߹ٴ��̴�."))
        {
            displayContent.text = content;
            displayCost.text = cost.ToString() + "$";
        }
    }

    public void Update()
    {
        
    }

    public void ItemClick()
    {
        // ���� ���� Ŭ���� ��� ����
        if (nowOn == gameObject.name)
            return;

        // ������ ��Ͽ��� ���� �����ִ� �������� ��
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == nowOn)
            {
                items[i].Value.gameObject.SetActive(false);
                break;
            }
        }
        
        // ������ ��Ͽ��� Ŭ���� �������� Ŵ
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Key == gameObject.name)
            {
                items[i].Value.gameObject.SetActive(true);
                nowOn = items[i].Key;                       // ������ ������ ����

                // ������ �����۸� ���� ���λ��׿� ǥ��
                displayContent.text = content;
                displayCost.text = cost.ToString() + "$";
                break;
            }
        }
    }
}