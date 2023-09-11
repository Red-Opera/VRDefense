using System;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseEffect : MonoBehaviour
{
    public GameObject coin;                     // ���� ����Ʈ�� ������ ������Ʈ
    public Text displayCost;                    // ���� ����� ǥ���� Text
    public Text displayMoney;                   // ���� �ݾ��� ǥ���� Text

    public float moveSpeed;                     // ������ �̵� �ӵ�
    public float fadeSpeed;                     // ����� �� ���������� �ӵ�
    public static bool isPlayCoin = false;      // ���� ����Ʈ ��� ����
    
    private Renderer renderer;                  // ������ ������
    private AudioSource audio;                  // ����� �÷��̾�

    private float currentAlpha = 1.0f;          // ���� ������ ����

    public void Start()
    {
        audio = GetComponent<AudioSource>();

        Debug.Assert(coin != null, "Error (GameObject is Null) : ���� ������Ʈ�� �������� �ʽ��ϴ�.");
        Debug.Assert(audio != null, "Error (AudioSource is Null) : ������� �������� �ʽ��ϴ�.");

        renderer = coin.GetComponent<Renderer>();
        coin.SetActive(false);
    }

    public void Update()
    {
        if(displayMoney.text!= GameManager.gameManager.currency.ToString() + "$")
        {
            displayMoney.text = GameManager.gameManager.currency.ToString() + "$";
        }
        // ������ �������� ���
        if (isPlayCoin)
            CoinRise();
    }

    public void BuyItem()
    {
        int getCurrency = GameManager.gameManager.currency;
        int getCost = Int32.Parse(displayCost.text.Substring(0, displayCost.text.Length - 1));

        // ��ȭ�� ������ ���
        if (getCurrency < getCost)
        {
            // ���� ���� �Ҹ� ���
            audio.Play();

            return;
        }

        // ������ ������ ����
        ShopButton.BuyItem(getCost);
        
        UiManager.uiManager.UpdateCurrencyText(GameManager.gameManager.currency);
        // ����� �������� �Ǵ��Ͽ� ������ �ٲ�
        if (getCost > GameManager.gameManager.currency)
            displayCost.color = Color.red;

        else
            displayCost.color = Color.green;

        isPlayCoin = true;
    }

    public void CoinRise()
    {
        // ������ �ö� �� Start
        if (!coin.activeSelf)
        {
            coin.SetActive(true);
            coin.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

            // ������ �ٽ� ���̰� ����
            currentAlpha = 1.0f;
            Color getColor = renderer.material.color;
            getColor.a = currentAlpha;
            renderer.material.color = getColor;
        }

        // ������Ʈ�� ���� �̵�
        coin.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // ������ ���ҽ�Ű��
        currentAlpha -= fadeSpeed * Time.deltaTime;

        // ������Ʈ �������� ���� ����
        Color currentColor = renderer.material.color;
        currentColor.a = Mathf.Max(currentAlpha, 0);
        renderer.material.color = currentColor;

        // ������ �ּҰ� ���Ϸ� �������� ������Ʈ �ı�
        if (currentAlpha <= 0)
        {
            coin.SetActive(false);
            isPlayCoin = false;
        }
    }
}
